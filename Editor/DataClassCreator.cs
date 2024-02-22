using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    public class DataClassCreator
    {
        const string FileName = "Data.template";
        readonly string _templateLocation = Path.Combine(Application.dataPath, "ScriptableComponentData", "Templates");
        const string SinglePropertyTemplate = "public DataEntry<{0}> {1};";
        const string ActionRegisterTemplate = "\t\t\t{0}.RegisterAction(DataUpdated);";
        const string ActionUnregisterTemplate = "\t\t\t{0}.UnregisterAction();";

        public string CreateDataClass(string className,
            IEnumerable<MemberInfo> memberInfos, string namespaceName, Type componentType)
        {
            var properties = new List<string>();
            var actionRegisters = new List<string>();
            var actionUnregisters = new List<string>();
            foreach (var memberInfo in memberInfos)
            {
                var propertyLine = string.Format(SinglePropertyTemplate, memberInfo.GetTypeName(), memberInfo.Name);

                if (memberInfo.DeclaringType != componentType)
                {
                    propertyLine = $"[Inherited] {propertyLine}";
                }
                
                propertyLine = $"\t\t{propertyLine}";
                
                properties.Add(propertyLine);

                actionRegisters.Add(string.Format(ActionRegisterTemplate, memberInfo.Name));
                actionUnregisters.Add(string.Format(ActionUnregisterTemplate, memberInfo.Name));
            }


            var fileTemplate = Utilities.ReadOrFindAndReadFile(_templateLocation, FileName);
            var result = fileTemplate
                .Replace("{path}", namespaceName.Replace(".", "/"))
                .Replace("{target_short}", className)
                .Replace("{namespace}", $"Data{namespaceName}")
                .Replace("{properties}", string.Join(Environment.NewLine, properties))
                .Replace("{action_registers}", string.Join(Environment.NewLine, actionRegisters))
                .Replace("{action_unregisters}", string.Join(Environment.NewLine, actionUnregisters));

            return result;
        }
    }
}