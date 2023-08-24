using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

public class GameEventManager : MonoSingleton<GameEventManager>
{
	public AllGameEvent allGameEvent;
	public Dictionary<string, EventSO> gameEventDic = new Dictionary<string, EventSO>();
	private bool isInit = false;

	private void Init()
	{
		allGameEvent = Resources.Load<AllGameEvent>("Assets/Resources/AllGameEvent.asset");
		foreach (var gameEvent in allGameEvent.gameEventList)
		{
			gameEventDic.Add(gameEvent.name, gameEvent);
		}
		isInit = true;
	}

	public EventSO GetGameEvent(string key)
	{
		if(!isInit)
		{
			Init();
		}
		return gameEventDic[key];
	}
}
