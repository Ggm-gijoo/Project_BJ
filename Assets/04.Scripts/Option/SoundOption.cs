using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOption
{
	public AudioMixer AudioMixer
	{
		get
		{
			return audioMixer;
		}
		set
		{
			audioMixer = value;
		}
	}
	private AudioMixer audioMixer;

	public void SetBGMVolume(float volume)
	{
		audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
		OptionManager.Instance.SaveOptionData.volumeBGM = volume;
	}

	public void SetEFFVolume(float volume)
	{
		audioMixer.SetFloat("EFFVolume", Mathf.Log10(volume) * 20);
		OptionManager.Instance.SaveOptionData.volumeEFF = volume;
	}
}
