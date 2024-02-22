using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    public static class DataBaseApproachHelper
    {
        [MenuItem("Core/Convert All Classes")]
        public static void ConvertAllClasses()
        {
            foreach (var type in GetEnumerableOfType<Component>())
            {
                Converter.CreateClasses(type, false);
            }
            AssetDatabase.Refresh();
        }
        static IEnumerable<Type> GetEnumerableOfType<T>(params object[] constructorArgs)
            where T : class
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .ToList();

            return types;
        }
    }
}