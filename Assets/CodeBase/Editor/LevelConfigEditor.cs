using CodeBase.Configs;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfig))]
public class LevelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelConfig config = (LevelConfig)target;

        GUILayout.Space(10);

        // Кнопка для связывания с текущей открытой сценой
        if (GUILayout.Button("Link to Current Scene"))
        {
            SceneAsset currentScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(
                EditorSceneManager.GetActiveScene().path
            );
            config.SetLinkedScene(currentScene);
        }
    }
}