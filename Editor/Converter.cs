using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    public static class Converter
    {
        [MenuItem("CONTEXT/Object/Convert Data To ScriptableObject")]
        public static void Convert(MenuCommand command)
        {
            CreateClasses(command.context.GetType(), true);
        }

        public static void CreateClasses(Type componentType, bool refresh)
        {
            if(!componentType.IsVisible)
                return;
            
            var members = GetMembers(componentType, BindingFlags.Public | BindingFlags.Instance).ToList();
            members = members.OrderByDescending(x => x.DeclaringType == componentType).ToList();
            if (members.Count == 0)
                return;
            
            var assemblyName = componentType.Assembly.GetName().Name;
            var namespaceName = componentType.Namespace ?? componentType.Name;

            var baseClassName = componentType.Name;
            var folderName = Path.Combine(Application.dataPath, "Generated", assemblyName);
            Directory.CreateDirectory(folderName);
            CreateDataClass(baseClassName, members, folderName, namespaceName, componentType);
            CreateComponentClass(componentType, baseClassName, members, folderName, namespaceName);
            if (refresh) AssetDatabase.Refresh();
        }

        static void CreateComponentClass(Type objectType, string baseClassName,
            IEnumerable<MemberInfo> memberInfos, string folderName, string namespaceName)
        {
            var componentClassCreator = new ComponentClassCreator();
            var result = componentClassCreator.CreateComponentClass(objectType, memberInfos, namespaceName);
            var path = Path.Combine(folderName, $"DataBased{baseClassName}.cs");
            File.WriteAllText(path, result);
        }

        static void CreateDataClass(string baseClassName, IEnumerable<MemberInfo> memberInfos, string folderName,
            string namespaceName, Type componentType)
        {
            var className = $"{baseClassName}Data";
            var dataClassCreator = new DataClassCreator();
            var result = dataClassCreator.CreateDataClass(baseClassName, memberInfos, namespaceName, componentType);
            var path = Path.Combine(folderName, $"{className}.cs");
            File.WriteAllText(path, result);
        }

        static IEnumerable<MemberInfo> GetMembers(Type objectType, BindingFlags flags)
        {
            return objectType
                .GetMembers(flags)
                .Where(IsMemberViable);
        }

        static bool IsMemberViable(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType != MemberTypes.Property && memberInfo.MemberType != MemberTypes.Field)
                return false;
            
            if (memberInfo is PropertyInfo { SetMethod: not { IsPublic: true } }) 
                return false;
            if ((memberInfo as PropertyInfo)?.PropertyType.IsInterface == true)
                return false;
            if (memberInfo.GetCustomAttributes(false).Any(x => x is ObsoleteAttribute))
                return false;
            
            return true;
        }
    }
}