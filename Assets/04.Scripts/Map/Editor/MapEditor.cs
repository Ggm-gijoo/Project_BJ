using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using UnityEditor.Compilation;
using System;

namespace Map
{
	public class MapEditor : EditorWindow
	{
		private static MapEditor mapEditor;
		private static AllMapDataSO allMapDataSO;
		private int cellSize = 32; // The size of each grid cell.
		private Color gridColor = Color.gray; // The color of the grid lines.
		private int lineWidth = 5; // The width of the grid lines.
		private Vector2Int gridOffset = Vector2Int.zero; // The top-left position of the visible grid area.
		
		[MenuItem("Tools/MapEditor")]
		static void Open()
		{
			// 생성되어있는 윈도우를 가져온다. 없으면 새로 생성한다. 싱글턴 구조인듯하다.
			mapEditor = (MapEditor)EditorWindow.GetWindow(typeof(MapEditor));
			allMapDataSO = Resources.Load<AllMapDataSO>("MapDatas/AllMapDataSO");
			mapEditor.Show();
		}
		private void OnGUI()
		{
			// Handle input for moving the view.
			HandleInput();

			// Draw the grid lines.
			DrawGrid();
			HandleGridClick();
		}
		private void HandleInput()
		{
			if (Event.current.type == EventType.KeyDown)
			{
				Vector2Int moveDirection = Vector2Int.zero;

				// Detect arrow key presses to move the view.
				if (Event.current.keyCode == KeyCode.LeftArrow)
					moveDirection.x = -1;
				else if (Event.current.keyCode == KeyCode.RightArrow)
					moveDirection.x = 1;
				else if (Event.current.keyCode == KeyCode.UpArrow)
					moveDirection.y = -1;
				else if (Event.current.keyCode == KeyCode.DownArrow)
					moveDirection.y = 1;

				// Update the gridOffset based on the moveDirection.
				gridOffset += moveDirection;

				// Repaint the editor window to update the visible grid area.
				Repaint();
			}
		}

		private void DrawGrid()
		{
			Handles.color = gridColor;
			Handles.BeginGUI();

			int xCorrection = (allMapDataSO.gridSize.x * cellSize / 2);
			int yCorrection = (allMapDataSO.gridSize.y * cellSize / 2);
			int startX = gridOffset.x * cellSize - xCorrection;
			int startY = gridOffset.y * cellSize - yCorrection;
			int endX = startX + xCorrection * 2;
			int endY = startY + yCorrection * 2;


			for (int x = startX; x <= endX; x += cellSize)
			{
				Handles.DrawAAPolyLine(lineWidth, new Vector2(x, startY), new Vector2(x, startY + allMapDataSO.gridSize.y * cellSize));
			}

			for (int y = startY; y <= endY; y += cellSize)
			{
				Handles.DrawAAPolyLine(lineWidth, new Vector2(startX, y), new Vector2(startX + allMapDataSO.gridSize.x * cellSize, y));
			}
			foreach(var obj in allMapDataSO.mapDataDic)
			{
				Texture2D tex = Resources.Load<Texture2D>($"MapDatas/Icon/{obj.Value.iconType.ToString()}");
				GUI.DrawTexture(new Rect(Vector2ToGridPos(obj.Key), new Vector2(cellSize, cellSize)), tex);
			}
			Handles.EndGUI();
		}

		private Vector2 Vector2ToGridPos(Vector2 vector2)
		{
			var x = (gridOffset.x + vector2.x) * cellSize - cellSize / 2;
			var y = (gridOffset.y + vector2.y) * cellSize - cellSize / 2;

			return new Vector2(x, y);
		}

		private void HandleGridClick()
		{
			Event e = Event.current;
			if (e.type == EventType.MouseDown && e.button == 0) // Left mouse button clicked
			{
				Vector2 mousePosition = e.mousePosition;

				// Check if the click is within the visible grid area.
				int startX = gridOffset.x * cellSize;
				int startY = gridOffset.y * cellSize;
				int xCorrection = (allMapDataSO.gridSize.x * cellSize / 2);
				int yCorrection = (allMapDataSO.gridSize.y * cellSize / 2);
				int endX = startX + xCorrection;
				int endY = startY + yCorrection;

				if (mousePosition.x >= startX - xCorrection && mousePosition.x <= endX &&
					mousePosition.y >= startY - yCorrection && mousePosition.y <= endY)
				{
					// Calculate the grid coordinates of the clicked cell.
					int gridX = Mathf.RoundToInt((mousePosition.x - startX) / cellSize);
					int gridY = Mathf.RoundToInt((mousePosition.y - startY) / cellSize);

					Debug.Log("Clicked Grid Cell: (" + gridX + ", " + gridY + ")");

					if (gridX == (allMapDataSO.gridSize.x / 2) || gridX == -(allMapDataSO.gridSize.x / 2))
					{
						allMapDataSO.gridSize.x += 2;
					}

					if (gridY == (allMapDataSO.gridSize.y / 2) || gridY == -(allMapDataSO.gridSize.y / 2))
					{
						allMapDataSO.gridSize.y += 2;
					}

					Vector2 pos = new Vector2(gridX, gridY);

					if (allMapDataSO.mapDataDic.ContainsKey(pos))
					{
						//인스펙터에 SO 정보 표시
						var so = allMapDataSO.mapDataDic[pos];
						//var scene = EditorSceneManager.GetSceneByName(so.sceneName);
						//EditorSceneManager.SetActiveScene(scene);
						string[] guids = AssetDatabase.FindAssets("t:Scene " + so.sceneName);
						Selection.activeObject = so;

						if (guids.Length > 0)
						{
							string path = "";
							string scenePath = "";
							for(int i = 0; i < guids.Length; ++i)
							{
								path = AssetDatabase.GUIDToAssetPath(guids[i]);
								if (path.Contains(so.sceneName))
								{
									scenePath = path;
								}
							}
							if(scenePath == "")
							{
								Debug.LogWarning("Scene not found: " + so.sceneName);
								Debug.LogWarning("Scene not found: " + guids[0]);
								return;
							}
							// Open the scene in the Unity Editor
							EditorSceneManager.OpenScene(scenePath);
						}
						else
						{
							Debug.LogWarning("Scene not found: " + so.sceneName);
						}
					}
					else
					{
						//SO 생성 및 SO 정보 표시
						var so = MapDataSO.CreateAsset(pos);
						allMapDataSO.AddMapDataSO(pos, so);
						EditorUtility.SetDirty(allMapDataSO);
						AssetDatabase.SaveAssets();
						Selection.activeObject = so;
						AssetDatabase.Refresh();
					}

				}
			}
		}
	}

}