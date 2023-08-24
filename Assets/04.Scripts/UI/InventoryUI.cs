using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
	[SerializeField]
	private GameObject blackMarker;
	[SerializeField]
	private GameObject gravityMarker;
	[SerializeField]
	private GameObject rubberMarker;

	[SerializeField]
	private GameObject blackMarkerUpgrade1;
	[SerializeField]
	private GameObject blackMarkerUpgrade2;
	[SerializeField]
	private GameObject blackMarkerUpgrade3;

	[SerializeField]
	private GameObject gravityMarkerUpgrade1;
	[SerializeField]
	private GameObject gravityMarkerUpgrade2;
	[SerializeField]
	private GameObject gravityMarkerUpgrade3;

	[SerializeField]
	private GameObject rubberMarkerUpgrade1;
	[SerializeField]
	private GameObject rubberMarkerUpgrade2;
	[SerializeField]
	private GameObject rubberMarkerUpgrade3;

	[SerializeField]
	private GameObject gun;
	[SerializeField]
	private GameObject bigGun;

	[SerializeField]
	private TextMeshProUGUI itemText;

	public void UpdateUI()
	{
		var data = InventoryManager.Instance.inventoryData;
		blackMarker.SetActive(false);
		gravityMarker.SetActive(false);
		rubberMarker.SetActive(false);
		blackMarkerUpgrade1.SetActive(false);
		blackMarkerUpgrade2.SetActive(false);
		blackMarkerUpgrade3.SetActive(false);
		gravityMarkerUpgrade1.SetActive(false);
		gravityMarkerUpgrade2.SetActive(false);
		gravityMarkerUpgrade3.SetActive(false);
		rubberMarkerUpgrade1.SetActive(false);
		rubberMarkerUpgrade2.SetActive(false);
		rubberMarkerUpgrade3.SetActive(false);
		gun.SetActive(false);
		bigGun.SetActive(false);
		itemText.text = "";

		if (data.isGetBlackMarker)
		{
			blackMarker.SetActive(true);
			if (data.upgradeBlackMarker > 0)
			{
				blackMarkerUpgrade1.SetActive(true);
			}
			if (data.upgradeBlackMarker > 1)
			{
				blackMarkerUpgrade2.SetActive(true);
			}
			if (data.upgradeBlackMarker > 2)
			{
				blackMarkerUpgrade3.SetActive(true);
			}
		}
		if (data.isGetGravityMarker)
		{
			gravityMarker.SetActive(true);
			if (data.upgradeGravityMarker > 0)
			{
				gravityMarkerUpgrade1.SetActive(true);
			}
			if (data.upgradeGravityMarker > 1)
			{
				gravityMarkerUpgrade2.SetActive(true);
			}
			if (data.upgradeGravityMarker > 2)
			{
				gravityMarkerUpgrade3.SetActive(true);
			}
		}
		if (data.isGetRubberMarker)
		{
			rubberMarker.SetActive(true);
			if (data.upgradeRubberMarker > 0)
			{
				rubberMarkerUpgrade1.SetActive(true);
			}
			if (data.upgradeRubberMarker > 1)
			{
				rubberMarkerUpgrade2.SetActive(true);
			}
			if (data.upgradeRubberMarker > 2)
			{
				rubberMarkerUpgrade3.SetActive(true);
			}
		}

		if(data.isGetGun)
		{
			gun.SetActive(true);
		}

		if(data.isGetBigGun)
		{
			bigGun.SetActive(true);
		}
	}

	public void ShowText(string str)
	{
		itemText.text = str;
		//
	}
}
