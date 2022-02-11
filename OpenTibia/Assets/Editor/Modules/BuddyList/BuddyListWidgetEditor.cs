using UnityEngine;
using UnityEditor;

namespace OpenTibiaUnityEditor.Modules.BuddyList
{
    [CustomEditor(typeof(OpenTibiaUnity.Modules.BuddyList.BuddyListWidget), true)]
    [CanEditMultipleObjects]
    public class BuddyListWidgetEditor : UI.Legacy.SidebarWidgetEditor
    {
        SerializedProperty _buddyList;

        protected override void OnEnable() {
            base.OnEnable();

            _buddyList = serializedObject.FindProperty("_buddyList");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("BuddyList Widget", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_buddyList, new GUIContent("Buddy List"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
