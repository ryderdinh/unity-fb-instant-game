using BZ_FIG_SDK.Scripts;
using UnityEditor;
using UnityEngine;

namespace BZ_FIG_SDK.Editor
{
    [CustomEditor(typeof(BZ_FIG_MANAGER))]
    [CanEditMultipleObjects]
    public class SDKManagerEditor : UnityEditor.Editor {
        

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUILayout.Separator();
           
            EditorGUILayout.Separator();
            EditorGUILayout.HelpBox("Hi", MessageType.Info);
            if (GUILayout.Button("Docs", new GUILayoutOption[] { GUILayout.Width(200) })) {
                Application.OpenURL("https://abc.com");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}