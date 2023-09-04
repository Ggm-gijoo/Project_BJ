using DG.Tweening;
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
			Middle,
		}

		public Vector2 CurrentPos
		{
			get
			{
				return currentPos;
			}
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

		[SerializeField]
		private Material sceneMaterial;

		[SerializeField]
		private Renderer screenRenderer;

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
			screenRenderer.material = sceneMaterial;
		}

		private void Init()
		{
			isInit = true;
			allMapDataSO = Resources.Load<AllMapDataSO>("MapDatas/AllMapDataSO");
		}

		public void MoveScene(MoveType moveType)
		{
			praviousePos = currentPos;
			//이동할 씬 설정
			switch (moveType)
			{
				case MoveType.Left:
					currentPos.x += -1;
					currentMoveType = moveType;
					break;
				case MoveType.Right:
					currentPos.x += 1;
					currentMoveType = moveType;
					break;
				case MoveType.Up:
					currentPos.y += -1;
					currentMoveType = moveType;
					break;
				case MoveType.Down:
					currentPos.y += 1;
					currentMoveType = moveType;
					break;
				case MoveType.Middle:
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
			FadeOutScene();
			yield return new WaitForSeconds(1f);
			if (!string.IsNullOrEmpty(allMapDataSO.mapDataDic[praviousePos].sceneName))
			{
				var op = SceneManager.UnloadSceneAsync(allMapDataSO.mapDataDic[praviousePos].sceneName);
				while (!op.isDone)
				{
					yield return null;
				}
			}
			Time.timeScale = 0f;
			var op2 = SceneManager.LoadSceneAsync(allMapDataSO.mapDataDic[currentPos].sceneName, LoadSceneMode.Additive);
			op2.allowSceneActivation = false;
			while(op2.progress < 0.9f)
			{
				yield return null;
			}
			Time.timeScale = 1f;
			op2.allowSceneActivation = true;
			FadeInScene();

			//Time.timeScale = 1f;
			yield return null;
		}


		public void TeleportScene()
		{
			//제작중
		}

		public void FadeOutScene()
		{
			DoFade(10, -10f, 1f);
		}
		public void FadeInScene()
		{
			DoFade(-10f, 10, 1f);
		}

		private void DoFade(float start, float dest, float time)
		{
			var mat = screenRenderer.material;
			DOTween.To(() => start, x => mat.SetFloat("_SpiltValue", x), dest, time);
		}
	}

}