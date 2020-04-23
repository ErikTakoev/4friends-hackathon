using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.UI;

[CustomEditor(typeof(PSDImporter))]
public class PSDImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PSDImporter importer = (PSDImporter)target;
        importer.ArtFolder = EditorGUILayout.TextField("Art Folder:", importer.ArtFolder);
        importer.IsSprite = EditorGUILayout.Toggle("Is Sprite:", importer.IsSprite);

        EditorGUILayout.LabelField("Positions Data:");
        importer.PositionsData = EditorGUILayout.TextArea(importer.PositionsData, GUILayout.Height(200));
        //base.OnInspectorGUI();

        if(GUILayout.Button("Create Nodes"))
        {
            CreateNodes(importer);
        }
    }

    private void CreateNodes(PSDImporter importer)
    {
        string[] lines = importer.PositionsData.Split(']');

        int i = 0;
        foreach (var line in lines)
        {
            string[] values = line.Replace("\n", "").Split('|');
            if (values.Length != 3) continue;

            string name = values[0];
            float x = float.Parse(values[1], CultureInfo.InvariantCulture);
            float y = float.Parse(values[2], CultureInfo.InvariantCulture);

            Transform find = FindNode(importer.transform, name);
            if (find)
                ChangeNode(find, x, y, i);
            else
                CreateNode(importer, name, x, y, i);
            //Debug.Log("Name:" + values[0] + ", x:" + x + ", y:" + y);
            i++;
        }
    }

    private void ChangeNode(Transform find, float x, float y, int sort)
    {
        Sprite sprite = null;
        SpriteRenderer spriteRenderer = null;
        Image image = null;

        find.TryGetComponent<SpriteRenderer>(out spriteRenderer);
        if (spriteRenderer)
            sprite = spriteRenderer.sprite;

        find.TryGetComponent<Image>(out image);
        if (image)
            sprite = image.sprite;


        if (!sprite) Debug.LogError("Sprite not find:" + find.name);

        find.localPosition = new Vector3(x + sprite.texture.width * 0.5f, y + sprite.texture.height * 0.5f, -sort * 0.001f);
    }

    Transform FindNode(Transform transform, string name)
    {
        Transform find = transform.Find(name);
        if (find) return find;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tr = transform.GetChild(i);

            Transform find2 = FindNode(tr, name);
            if (find2) return find2;
        }

        return null;
    }

    void CreateNode(PSDImporter importer, string name, float x, float y, int sort)
    {
        var go = new GameObject(name);
        go.transform.parent = importer.transform;

        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(importer.ArtFolder, name + ".png"));
        if (importer.IsSprite)
        {
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
        }
        else
        {
            Image image = go.AddComponent<Image>();
            image.sprite = sprite;
            image.rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
        }
        go.transform.localPosition = new Vector3(x + sprite.texture.width * 0.5f, y + sprite.texture.height * 0.5f, -sort * 0.001f);
        go.transform.localScale = Vector3.one;

        
    }
}
