using UnityEngine;
using UnityEditor;

namespace OpenTibiaUnityEditor.Modules.BuddyList
{
    [CustomEditor(typeof(OpenTibiaUnity.Modules.BuddyList.BuddyListAddWidget), true)]
    [CanEditMultipleObjects]
    public class BuddyListAddWidgetEditor : UI.Legacy.PopUpBaseEditor
    {
        SerializedProperty _name;

        protected override void OnEnable() {
            base.OnEnable();

            _name = serializedObject.FindProperty("_name");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("BuddyListAddWidget", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_name, new GUIContent("Name"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
