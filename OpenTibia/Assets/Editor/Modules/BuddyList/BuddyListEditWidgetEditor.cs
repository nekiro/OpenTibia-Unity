using UnityEngine;
using UnityEditor;

namespace OpenTibiaUnityEditor.Modules.BuddyList
{
    [CustomEditor(typeof(OpenTibiaUnity.Modules.BuddyList.BuddyListEditWidget), true)]
    [CanEditMultipleObjects]
    public class BuddyListEditWidgetEditor : UI.Legacy.PopUpBaseEditor
    {
        SerializedProperty _nameLabel;
        SerializedProperty _desc;
        SerializedProperty _iconsHolder;

        protected override void OnEnable() {
            base.OnEnable();

            _nameLabel = serializedObject.FindProperty("_nameLabel");
            _desc = serializedObject.FindProperty("_desc");
            _iconsHolder = serializedObject.FindProperty("_iconsHolder");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("BuddyListEditWidget", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_nameLabel, new GUIContent("Name"));
            EditorGUILayout.PropertyField(_desc, new GUIContent("Desc"));
            EditorGUILayout.PropertyField(_iconsHolder, new GUIContent("Icons Holder"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
