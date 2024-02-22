using System.Collections.Generic;
using System.Reflection;
using ComponentDatafier.Runtime;
using UnityEditor;
using UnityEngine;

namespace ComponentDatafier.Editor
{
    [CustomEditor(typeof(DataObject), true)]
    public class DataObjectInspector : UnityEditor.Editor
    {
        readonly Color _firstColor = new(0, 0, 0, .1f);
        readonly Color _secondColor = new(0.5f, 0.5f, 0.5f, .1f);
        bool _showDeclaredMembers = true;
        bool _showInheritedMembers = false;

        readonly List<FieldInfo> _declaredMembers = new();
        readonly List<FieldInfo> _inheritedMembers = new();

        void OnEnable()
        {
            var fields = target.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.GetCustomAttributes(typeof(InheritedAttribute), false).Length > 0)
                    _inheritedMembers.Add(fieldInfo);
                else
                    _declaredMembers.Add(fieldInfo);
            }
        }

        public override void OnInspectorGUI()
        {
            var currentColor = _firstColor;
            EditorGUIUtility.labelWidth = 250.0f;

            _showDeclaredMembers = EditorGUILayout.Foldout(_showDeclaredMembers, "Declared Members", true);
            var rect = GUILayoutUtility.GetLastRect();

            if (_showDeclaredMembers)
            {
                foreach (var fieldInfo in _declaredMembers)
                {
                    rect = DrawSingleField(fieldInfo, rect, ref currentColor);
                }
            }

            _showInheritedMembers = EditorGUILayout.Foldout(_showInheritedMembers, "Inherited Members", true);
            rect = GUILayoutUtility.GetLastRect();

            if (_showInheritedMembers)
            {
                foreach (var fieldInfo in _inheritedMembers)
                {
                    rect = DrawSingleField(fieldInfo, rect, ref currentColor);
                }
            }

            EditorGUIUtility.labelWidth = 0;
        }

        Rect DrawSingleField(FieldInfo fieldInfo, Rect rect, ref Color currentColor)
        {
            var property = serializedObject.FindProperty($"{fieldInfo.Name}.value");
            var isActiveProperty = serializedObject.FindProperty($"{fieldInfo.Name}.isActive");
            var propertyContent = new GUIContent { text = fieldInfo.Name.CamelCaseToWords() };

            EditorGUILayout.BeginHorizontal();

            rect.DrawRect(EditorGUI.GetPropertyHeight(property), currentColor);

            isActiveProperty.boolValue = EditorGUILayout.Toggle(isActiveProperty.boolValue, GUILayout.Width(25));
            using (new EditorGUI.DisabledScope(!isActiveProperty.boolValue))
            {
                EditorGUILayout.PropertyField(property, propertyContent, true);
            }

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndHorizontal();

            rect = GUILayoutUtility.GetLastRect();
            currentColor = currentColor == _firstColor ? _secondColor : _firstColor;
            return rect;
        }
    }
}