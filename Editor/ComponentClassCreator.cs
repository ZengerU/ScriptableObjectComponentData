using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    public class ComponentClassCreator
    {
        const string FileName = "Component.template";
        readonly string _templateLocation = Path.Combine(Application.dataPath, "ComponentDatafier", "Templates");
        const string SinglePropertyTemplate = "\t\t\tif (data.{name}.isActive) Target.{name} = data.{name}.value;";
        
        public string CreateComponentClass(Type objectType,
            IEnumerable<MemberInfo> memberInfos, string namespaceName)
        {
            var properties = new List<string>();
            foreach (var memberInfo in memberInfos)
            {
                properties.Add(SinglePropertyTemplate.Replace("{name}", memberInfo.Name));
            }

            var fileTemplate = Utilities.ReadOrFindAndReadFile(_templateLocation, FileName);
            var result = fileTemplate
                .Replace("{namespace}", $"Data{namespaceName}")
                .Replace("{target}", objectType.FullName)
                .Replace("{properties}", string.Join(Environment.NewLine, properties))
                .Replace("{target_short}", objectType.Name);
            return result;
        }
    }
}