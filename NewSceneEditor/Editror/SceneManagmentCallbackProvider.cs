using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;

namespace NewSceneManagment
{
    [InitializeOnLoad]
    public class SceneManagmentCallbackProvider
    {
        static SceneManagmentCallbackProvider()
        {
            CustomScenePreferences.LoadPreferences();
            EditorSceneManager.newSceneCreated += NewSceneCreated;
            EditorSceneManager.sceneSaving += NewSceneSaving;
        }

        private static void NewSceneCreated(UnityEngine.SceneManagement.Scene scene, UnityEditor.SceneManagement.NewSceneSetup setup, UnityEditor.SceneManagement.NewSceneMode mode)
        {
            Debug.Log("New Scene Created");
            CustomScenePreferences.CreateNewScene();
        }
        private static void NewSceneSaving(UnityEngine.SceneManagement.Scene scene, string path)
        {
            Debug.Log("New Scene Saved");
            CustomScenePreferences.OnSceneSaving(scene, path);
        }
    }
}