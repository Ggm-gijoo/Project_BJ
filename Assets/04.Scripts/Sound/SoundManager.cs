using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utill;
using Utill.Pattern;

namespace Sound
{
	/// <summary>
	/// 효과음 및 배경음악을 재생하는 매니저
	/// </summary>
	public class SoundManager : MonoSingleton<SoundManager>
	{
		private AudioSource _effAudioSource = null;
		private EFFSO _effSO;

		private bool _isInit = false; //사운드 매니저 초기화 여부


		/// <summary>
		/// 효과음 출력 함수
		/// </summary>
		/// <param name="audioName"></param>
		public void PlayEFF(string audioName)
		{
			if (!_isInit)
			{
				Init();
			}

			//EffSO에서 효과음을 가져온다
			AudioClip clip = _effSO.GetEFFClip(audioName);
			_effAudioSource.PlayOneShot(clip);
		}

		private void Start()
		{
			if (!_isInit)
			{
				if (Instance == this)
				{
					Init();
				}
				else
				{
					Instance.Init();
				}
			}
		}

		/// <summary>
		/// 초기화
		/// </summary>
		private void Init()
		{
			if (_isInit)
			{
				return;
			}
			_isInit = true;

			_effSO = Resources.Load<EFFSO>("EFFSO");// AddressablesManager.Instance.GetResource<EFFSO>("EFFSO");
			GenerateEFFAudioSource();
		}

		/// <summary>
		/// 이펙트 오디오 소스들 생성
		/// </summary>
		private void GenerateEFFAudioSource()
		{
			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject("EFF");
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			//audioSource.outputAudioMixerGroup = _effAudioGroup;
			audioSource.clip = null;
			audioSource.playOnAwake = true;
			audioSource.loop = true;

			_effAudioSource = audioSource;
		}

	}

}