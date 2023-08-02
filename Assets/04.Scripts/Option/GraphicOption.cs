using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GraphicOption
{
	public void SetResolution(int width, int height)
	{
		Screen.SetResolution(width, height, Screen.fullScreen);
		OptionManager.Instance.SaveOptionData.width = width;
		OptionManager.Instance.SaveOptionData.height = height;
	}
	public void SetGraphicQuality(int qualityLevel)
	{
		QualitySettings.SetQualityLevel(qualityLevel);
		OptionManager.Instance.SaveOptionData.qualityLevel = qualityLevel;
	}
	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
		OptionManager.Instance.SaveOptionData.isFullScreen = isFullScreen;
	}
}
