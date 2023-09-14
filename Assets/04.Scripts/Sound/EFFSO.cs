using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 효과음 SO
/// </summary>
[CreateAssetMenu]
public class EFFSO : ScriptableObject
{
	[FormerlySerializedAs("effaudioClips")]
	public AudioClip[] _effaudioClips;
	[FormerlySerializedAs("audioDictionary")]
	private Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();

	/// <summary>
	/// 효과음 클립을 가져온다
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public AudioClip GetEFFClip(string name)
	{
		AudioClip clip = null;
		
		if(_audioDictionary.TryGetValue(name, out clip))
		{
			return clip;
		}

		//만약 지정한 소리가 없으면 초기화를 다시 한번 한다.
		SetEFFClips();
		
		if (_audioDictionary.TryGetValue(name, out clip))
		{
			return clip;
		}

		//효과음 없음
		Debug.LogError("없는 이름입니다");

		return null;
	}

	/// <summary>
	/// 현재 효과음 클립을 딕셔너리로 저장함
	/// </summary>
	[ContextMenu("SetEFFClips")]
	private void SetEFFClips()
	{
		_audioDictionary.Clear();
		foreach(var clip in  _effaudioClips)
		{
			_audioDictionary.Add(clip.name, clip);
		}
	}

}
