using UnityEngine;
using UnityEditor;
using Nexuris.Core;

namespace Nexuris.EditorTools
{
    [CustomEditor(typeof(DynamicTile))]
    public class DynamicTileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //would need a cache system if project grows large and needs optimisation for editor. 

            DrawDefaultInspector();

            EditorGUILayout.Space(15);
            EditorGUILayout.LabelField("Asset Pipeline Tool", EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.Space(5);

            DynamicTile tile = (DynamicTile)target;

            Sprite currentSprite = null;
            if (!string.IsNullOrEmpty(tile.runtimeTexturePath))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(tile.runtimeTexturePath);
                string[] guids = AssetDatabase.FindAssets($"{fileName} t:Sprite");

                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    // Match the end of the path to make sure it's the exact asset inside Resources
                    if (path.Contains(tile.runtimeTexturePath))
                    {
                        currentSprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                        if (currentSprite != null) break;
                    }
                }
            }

            EditorGUI.BeginChangeCheck();
            Sprite _selectedSprite = (Sprite)EditorGUILayout.ObjectField(
                "Drag Sprite Here",
                currentSprite,
                typeof(Sprite),
                false,
                GUILayout.Height(40)
            );

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tile, "Update DynamicTile Sprite Path");

                if (_selectedSprite != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(_selectedSprite);

                    if (assetPath.Contains("/Resources/"))
                    {
                        int resourcesIndex = assetPath.IndexOf("/Resources/") + "/Resources/".Length;
                        string cleanPath = assetPath.Substring(resourcesIndex);

                        int extensionIndex = cleanPath.LastIndexOf('.');
                        if (extensionIndex > 0)
                        {
                            cleanPath = cleanPath.Substring(0, extensionIndex);
                        }

                        tile.runtimeTexturePath = cleanPath;
                    }
                    else
                    {
                        Debug.LogError("[DynamicTileEditor] ERROR: The asset you dragged does not live inside a 'Resources' folder.");
                    }
                }
                else
                {
                    tile.runtimeTexturePath = "";
                }

                EditorUtility.SetDirty(tile);
                AssetDatabase.SaveAssets();
            }
        }
    }
}