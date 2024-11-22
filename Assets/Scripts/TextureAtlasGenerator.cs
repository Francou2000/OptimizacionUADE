using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureAtlasGenerator : MonoBehaviour
{
    public Material atlasMat;
    public Texture2D[] textures; // Texturas a combinar
    public int atlasWidth = 1024; // Ancho del atlas
    public int atlasHeight = 1024; // Alto del atlas

    [ContextMenu("Pack")]
    void PackTextures()
    {
        Texture2D atlas = new Texture2D(atlasWidth, atlasHeight);
        Rect[] rects = atlas.PackTextures(textures, 1); // Empaqueta las texturas

        string path = EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", "Atlas3D", "asset");
        if (string.IsNullOrEmpty(path)) return;

        path = FileUtil.GetProjectRelativePath(path);

        AssetDatabase.CreateAsset(atlas, path);
        AssetDatabase.SaveAssets();
    }
}
