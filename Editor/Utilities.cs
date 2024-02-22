using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    public static class Utilities
    {
        public static string ReadOrFindAndReadFile(string defaultPath, string fileName)
        {
            var path = Path.Combine(defaultPath, fileName);
            if (!File.Exists(path))
            {
                var files = Directory.GetFiles(Application.dataPath, fileName, SearchOption.AllDirectories);
                path = files[0];
            }
            
            return File.ReadAllText(path);
        }
        public static string GetTypeName(this MemberInfo memberInfo)
        {
            var typeName = string.Empty;

            if (memberInfo is PropertyInfo propertyInfo)
            {
                typeName = propertyInfo.PropertyType.FullName;
            }

            if (memberInfo is FieldInfo fieldInfo)
            {
                typeName = fieldInfo.FieldType.FullName;
            }

            if (typeName.Contains("`1[["))
            {
                var nestCount = Regex.Matches(typeName, @"`1\[\[").Count;
                typeName = typeName.Replace("`1[[", "<");
                typeName = typeName.Split(",")[0];
                for (var i = 0; i < nestCount; i++)
                {
                    typeName += ">";
                }
            }

            typeName = typeName.Replace("+", ".");
            return typeName;
        }
        public static void DrawRect(this Rect rect, float height, Color currentColor)
        {
            rect.x = 15;
            rect.y += rect.height + 2;
            rect.width = Screen.width - 20;
            rect.height = height;

            EditorGUI.DrawRect(rect, currentColor);
        }
        public static string CamelCaseToWords(this string input)
        {
            var result = Regex.Replace(input, "(\\B[A-Z])", " $1");
            return FirstCharToUpper(result);
        }
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input[0].ToString().ToUpper() + input[1..]
            };
    }
}