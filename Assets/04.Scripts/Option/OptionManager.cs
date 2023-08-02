using Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using Utill.Addressable;
using Utill.Pattern;

public class OptionManager : MonoSingleton<OptionManager>
{
	public SaveOptionData SaveOptionData => saveOptionData;
	public GraphicOption GraphicOption => graphicOption;
	public SoundOption SoundOption => soundOption;

	private GraphicOption graphicOption;
	private SoundOption soundOption;
	private SaveOptionData saveOptionData;

	private void Start()
	{
		graphicOption = new GraphicOption();
		soundOption = new SoundOption();
		soundOption.AudioMixer = AddressablesManager.Instance.GetResource<AudioMixer>("MainMixer");

		LoadSaveOptionData();
	}

	private void LoadSaveOptionData()
	{
		saveOptionData = new SaveOptionData();
		if(StaticSave.GetCheckBool())
		{
			StaticSave.Load<SaveOptionData>(ref  saveOptionData, "OptionData");
		}
		graphicOption.SetResolution(saveOptionData.width, saveOptionData.height);
		graphicOption.SetFullScreen(saveOptionData.isFullScreen);
		graphicOption.SetGraphicQuality(saveOptionData.qualityLevel);
		soundOption.SetBGMVolume(saveOptionData.volumeBGM);
		soundOption.SetEFFVolume(saveOptionData.volumeEFF);
	}

	public void SetSaveOptionData()
	{
		StaticSave.Save<SaveOptionData>(ref saveOptionData, "OptionData");
	}
}
