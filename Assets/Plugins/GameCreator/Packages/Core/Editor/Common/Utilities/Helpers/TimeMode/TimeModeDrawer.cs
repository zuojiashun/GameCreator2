﻿using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using GameCreator.Runtime.Common;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(TimeMode))]
    public class TimeModeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty propertyUpdateTime = property.FindPropertyRelative("m_UpdateTime");
            PropertyTool fieldUpdateTime = new PropertyTool(propertyUpdateTime);

            return fieldUpdateTime;
        }
    }
}