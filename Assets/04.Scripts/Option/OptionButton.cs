using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionButton : MonoBehaviour
{
	public void SetVolumeEFF(float volume)
	{
		OptionManager.Instance.SoundOption.SetEFFVolume(volume);
	}

	public void SetVolumeBGM(float volume)
	{
		OptionManager.Instance.SoundOption.SetBGMVolume(volume);
	}

	public void SetResolution(int index)
	{
		switch (index)
		{
			default:
			case 0:
				SetResolution(1920, 1080);
				break;
			case 1:
				SetResolution(1280,720);
				break;
			case 2:
				SetResolution(854,480);
				break;
			case 3:
				SetResolution(640,360);
				break;
		}
		
	}
	public void SetResolution(int width, int height)
	{
		OptionManager.Instance.GraphicOption.SetResolution(width, height);
	}

	public void SetGraphicQuality(int quality)
	{
		OptionManager.Instance.GraphicOption.SetGraphicQuality(quality);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		OptionManager.Instance.GraphicOption.SetFullScreen(isFullScreen);
	}

	public void SetSave()
	{
		OptionManager.Instance.SetSaveOptionData();
	}
}
