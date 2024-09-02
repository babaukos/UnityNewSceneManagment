using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

namespace NewSceneManagment
{
    public class CustomScenePreferences : EditorWindow
    {
        public enum SceneType { Default, Parametric, Template }

        private static SceneType selectedSceneType = SceneType.Default;
        private static bool createCamera = true;
        private static bool createSkybox = true;
        private static bool createDirectionalLight = true;
        private static string templateScenePath = "Assets/Scenes/TemplateScene.unity";


        // Register the Preferences GUI in the Preferences window
        [PreferenceItem("New Scene")]
        public static void PreferencesGUI()
        {
            GUILayout.Label("New Scene Preferences", EditorStyles.boldLabel);

            selectedSceneType = (SceneType)EditorGUILayout.EnumPopup("Scene Type", selectedSceneType);

            if (selectedSceneType == SceneType.Parametric)
            {
                createCamera = EditorGUILayout.Toggle("Create Camera", createCamera);
                createSkybox = EditorGUILayout.Toggle("Create Skybox", createSkybox);
                createDirectionalLight = EditorGUILayout.Toggle("Create Directional Light", createDirectionalLight);
            }
            else if (selectedSceneType == SceneType.Template)
            {
                templateScenePath = EditorGUILayout.TextField("Template Scene Path", templateScenePath);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Pick Scene Template");
                if (GUILayout.Button("Pick Template", GUILayout.Width(100)))
                {
                    string path = EditorUtility.OpenFilePanel("Select Template Scene", "Assets/Scenes", "unity");
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (path.StartsWith(Application.dataPath))
                        {
                            templateScenePath = "Assets" + path.Substring(Application.dataPath.Length);
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUI.changed)
            {
                SavePreferences();
            }
        }

        private void OnDestroy()
        {
            SavePreferences();
        }

        // Save Preferences
        private static void SavePreferences()
        {
            EditorPrefs.SetInt("CustomSceneType", (int)selectedSceneType);
            EditorPrefs.SetBool("CreateCamera", createCamera);
            EditorPrefs.SetBool("CreateSkybox", createSkybox);
            EditorPrefs.SetBool("CreateDirectionalLight", createDirectionalLight);
            EditorPrefs.SetString("TemplateScenePath", templateScenePath);
        }

        // Load Preferences
        public static void LoadPreferences()
        {
            selectedSceneType = (SceneType)EditorPrefs.GetInt("CustomSceneType", (int)SceneType.Default);
            createCamera = EditorPrefs.GetBool("CreateCamera", true);
            createSkybox = EditorPrefs.GetBool("CreateSkybox", true);
            createDirectionalLight = EditorPrefs.GetBool("CreateDirectionalLight", true);
            templateScenePath = EditorPrefs.GetString("TemplateScenePath", "Assets/Scenes/TemplateScene.unity");
        }

        private static void ManageSceneObjects()
        {
            // Check for Camera preferences
            Camera camera = GameObject.FindObjectOfType<Camera>();
            if (!createCamera && camera != null)
            {
                DestroyImmediate(camera.gameObject);
            }

            // Check for Skybox on preferences
            if (createSkybox && RenderSettings.skybox == null)
            {
                Material mat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/DefaultSkybox.mat");
                RenderSettings.skybox = mat;
            }

            // Check for Light on preferences
            Light light = GameObject.FindObjectOfType<Light>();
            if (!createDirectionalLight && light != null)
            {
                DestroyImmediate(light.gameObject);
            }
        }
        public static void CreateNewScene()
        {
            LoadPreferences();

            if (selectedSceneType == SceneType.Parametric)
            {
                // Create or remove objects based on preferences
                ManageSceneObjects();
            }
            else if (selectedSceneType == SceneType.Template)
            {
                if (!string.IsNullOrEmpty(templateScenePath) && File.Exists(templateScenePath))
                {
                    EditorSceneManager.OpenScene(templateScenePath);
                }
                else
                {
                    Debug.LogError("Template scene not found at the specified path.");
                }
            }
        }
        public static void OnSceneSaving(UnityEngine.SceneManagement.Scene scene, string path)
        {
            // Check if the scene being saved is the template scene
            if (Path.GetFileNameWithoutExtension(path) == Path.GetFileNameWithoutExtension(templateScenePath))
            {
                // Prompt the user to save the scene with a new name
                string newPath = EditorUtility.SaveFilePanel("Save Scene As", Path.GetDirectoryName(path), scene.name, "unity");
                if (!string.IsNullOrEmpty(newPath))
                {
                    EditorSceneManager.SaveScene(scene, newPath);
                }

                // Cancel the default save operation to avoid overwriting the template
                EditorSceneManager.MarkSceneDirty(scene);
                //GUIUtility.ExitGUI();
            }
        }
    }
}
