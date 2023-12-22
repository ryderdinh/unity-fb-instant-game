using BZ_FIG_SDK.Scripts;
using UnityEditor;
using UnityEngine;

namespace BZ_FIG_SDK.Editor
{
    public class Menu
    {
        [MenuItem("BZ_FIG_SDK/Initialize SDK GameObject", false, 200)]
        private static void CreateNewGameObject()
        {
            AddSDK();
        }

        private static void AddSDK()
        {
            if (Object.FindObjectOfType(typeof(BZ_FIG_MANAGER)) == null)
            {
                var go = PrefabUtility.InstantiatePrefab(
                    AssetDatabase.LoadAssetAtPath("Assets/BZ_FIG_SDK/Prefabs/BZ_FIG_MANAGER.prefab",
                        typeof(BZ_FIG_MANAGER))) as BZ_FIG_MANAGER;

                if (go == null) return;

                go.name = "BZ_FIG_MANAGER";
                go.transform.SetSiblingIndex(1);
                Selection.activeObject = go;
                Undo.RegisterCreatedObjectUndo(go, "Created BZ_FIG_MANAGER Object");
            }
            else
            {
                Debug.Log(
                    "A BZ_FIG_SDK object already exists in this scene - you should never have more than one per scene!!");
            }
        }
    }
}