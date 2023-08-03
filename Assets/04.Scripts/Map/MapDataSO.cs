using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SceneTemplate;
#endif

namespace Map
{
	public enum IconType
	{
		None,
		MapIcon1,
		MapIcon2,
		MapIcon3,
		MapIcon4,
		MapIcon5,
		MapIcon6,
		Test,
	}

	[CreateAssetMenu(fileName = "MapDataSO", menuName = "SO/MapDataSO")]
	public class MapDataSO : ScriptableObject
	{
		public IconType iconType;
		public Vector2 pos;
		public string sceneName;

#if UNITY_EDITOR
		public static MapDataSO CreateAsset(Vector2 pos)
		{
			var exampleAsset = CreateInstance<MapDataSO>();
			exampleAsset.iconType = IconType.MapIcon1;
			exampleAsset.pos = pos;
			exampleAsset.sceneName = $"Map({pos.x},{pos.y})";

			AssetDatabase.CreateAsset(exampleAsset, $"Assets/Resources/MapDatas/Map({pos.x},{pos.y}).asset");

			Scene templateScene = EditorSceneManager.OpenScene("Assets/Resources/MapDatas/TemplateScene.unity", OpenSceneMode.Single);
			// Create a new scene with the desired name and path
			//Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

			// Save the scene to the specified path (modify this path as per your requirements)
			string scenePath = $"Assets/Resources/MapDatas/Scenes/Map({pos.x},{pos.y}).unity";
			EditorSceneManager.SaveScene(templateScene, scenePath);

			// Optionally, set the active scene to the new scene
			EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
			EditorBuildSettingsScene buildScene = new EditorBuildSettingsScene(scenePath, true);
			int newIndex = buildScenes.Length; // Add the scene at the end
			System.Array.Resize(ref buildScenes, newIndex + 1);
			buildScenes[newIndex] = buildScene;
			EditorBuildSettings.scenes = buildScenes;
			EditorSceneManager.OpenScene(scenePath);
			AssetDatabase.Refresh();
			return exampleAsset;
		}
#endif
	}
}
