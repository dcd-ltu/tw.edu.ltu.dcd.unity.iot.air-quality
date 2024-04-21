using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class CreateFolders : EditorWindow
{
    [MenuItem("Assets/Create Folder Templates(新增資料夾範本)")]
    private static void SetUpFolders()
    {
        CreateFolders windows = ScriptableObject.CreateInstance<CreateFolders>();
        windows.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 100);
        windows.ShowPopup();
    }

    private void OnGUI()
    {
        Color _backup = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;
        GUILayout.Label("利用該程式可以產生通用資料夾");
        GUILayout.Space(20);
        if (GUILayout.Button("產生資料夾!"))
        {
            CreateAllFolders();
            this.Close();
        }

        GUI.backgroundColor = _backup;
    }

    private static void CreateAllFolders()
    {
        try
        {
            CreateChildFolders(@"Assets", new List<string>()
            {
                @"Art",
                @"Audio",
                @"Level",
                @"Code",
            });

            CreateChildFolders(@"Assets/Art", new List<string>()
            {
                @"Animations",
                @"Models",
                @"Sprites",
            });
            CreateChildFolders(@"Assets/Art/Sprites", new List<string>()
            {
                @"AnimatedSprites",
                @"UI",
                @"Utilities",
            });
            CreateChildFolders(@"Assets/Art/Sprites/UI", new List<string>()
            {
                @"HUD",
                @"Menu",
            });
            CreateChildFolders(@"Assets/Audio", new List<string>()
            {
                @"Music",
                @"Sound",
            });

            CreateChildFolders(@"Assets/Level", new List<string>()
            {
                @"Prefabs",
                @"Scenes",
            });

            CreateChildFolders(@"Assets/Code", new List<string>()
            {
                @"Scripts",
                @"Shaders",
            });

            AssetDatabase.Refresh();
        }
        catch (Exception exp)
        {
            Debug.LogWarning(exp.ToString());
            throw;
        }
    }


    private static void CreateChildFolders(string rootPath, IList<string> childs)
    {
        foreach (string folder in childs)
        {
            if (!Directory.Exists(@$"{rootPath}/{folder}"))
            {
                Directory.CreateDirectory(@$"{rootPath}/{folder}");
            }
        }

        AssetDatabase.Refresh();
    }
}