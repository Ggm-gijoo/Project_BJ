using Map;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using static Map.MapMoveManager;

namespace Map
{
	public class MapMoveManager : MonoSingleton<MapMoveManager>
	{
		public enum MoveType
		{
			Left,
			Right,
			Up,
			Down,
		}

		public MoveType CurrentMoveType => currentMoveType;

		private Vector2 praviousePos;
		private Vector2 currentPos;
		private MoveType currentMoveType;
		private static AllMapDataSO allMapDataSO;
		private bool isInit;

		[SerializeField]
		private EventSO mapMoveEventSO;

		[SerializeField]
		private Vector2 editorLoadMapFirst;

		void Awake()
		{
			if (!isInit)
			{
				Init();
			}
		}
		private void Start()
		{
			SceneManager.LoadScene(allMapDataSO.mapDataDic[currentPos].sceneName, LoadSceneMode.Additive);
			currentPos = editorLoadMapFirst;
		}

		private void Init()
		{
			isInit = true;
			allMapDataSO = Resources.Load<AllMapDataSO>("MapDatas/AllMapDataSO");
		}

		public void MoveScene(MoveType moveType)
		{
			currentMoveType = moveType;
			praviousePos = currentPos;

			//이동할 씬 설정
			switch (moveType)
			{
				case MoveType.Left:
					currentPos.x += -1;
					break;
				case MoveType.Right:
					currentPos.x += 1;
					break;
				case MoveType.Up:
					currentPos.y += 1;
					break;
				case MoveType.Down:
					currentPos.y += -1;
					break;
			}

			//씬 이동
			if (allMapDataSO.mapDataDic.ContainsKey(currentPos))
			{
				StartCoroutine(LoadingScene(moveType));
			}
		}
		private IEnumerator LoadingScene(MoveType moveType)
		{
			if (!string.IsNullOrEmpty(allMapDataSO.mapDataDic[praviousePos].sceneName))
			{
				var op = SceneManager.UnloadSceneAsync(allMapDataSO.mapDataDic[praviousePos].sceneName);
				while (!op.isDone)
				{
					yield return null;
				}
			}
			SceneManager.LoadScene(allMapDataSO.mapDataDic[currentPos].sceneName, LoadSceneMode.Additive);
			//mapMoveEventSO.Raise();
		}


		public void TeleportScene()
		{
			//제작중
		}
	}

}