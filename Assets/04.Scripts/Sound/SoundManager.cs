using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utill;
using Utill.Pattern;

namespace Sound
{
	/// <summary>
	/// ȿ���� �� ��������� ����ϴ� �Ŵ���
	/// </summary>
	public class SoundManager : MonoSingleton<SoundManager>
	{
		private AudioSource _effAudioSource = null;
		private EFFSO _effSO;

		private bool _isInit = false; //���� �Ŵ��� �ʱ�ȭ ����


		/// <summary>
		/// ȿ���� ��� �Լ�
		/// </summary>
		/// <param name="audioName"></param>
		public void PlayEFF(string audioName)
		{
			if (!_isInit)
			{
				Init();
			}

			//EffSO���� ȿ������ �����´�
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
		/// �ʱ�ȭ
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
		/// ����Ʈ ����� �ҽ��� ����
		/// </summary>
		private void GenerateEFFAudioSource()
		{
			//���ο� ����� �ҽ� �����
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