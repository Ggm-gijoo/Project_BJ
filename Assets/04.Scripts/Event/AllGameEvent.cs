using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AllGameEvent : ScriptableObject
{
	public List<EventSO> gameEventList = new List<EventSO>();
}
