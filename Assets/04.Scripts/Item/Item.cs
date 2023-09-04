using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	None,
	Gun,
	BigGun,
	BlackMarker,
	GravityMarker,
	RubberMarker,
	BlackMarkerOne,
	BlackMarkerTwo,
	BlackMarkerThree,
	GravityMarkerOne,
	GravityMarkerTwo,
	GravityMarkerThree,
	RubberMarkerOne,
	RubberMarkerTwo,
	RubberMarkerThree,
}

public class Item : MonoBehaviour
{
	[SerializeField]
	private ItemType itemType;

	public void Start()
	{
		if(Check())
		{
			gameObject.SetActive(false);
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			GetItem();
			gameObject.SetActive(false);
		}
	}

	private void GetItem()
	{
		switch(itemType)
		{
			case ItemType.None:
				break;
			case ItemType.Gun:
				InventoryManager.Instance.inventoryData.isGetGun = true;
				break;
			case ItemType.BigGun:
				InventoryManager.Instance.inventoryData.isGetBigGun = true;
				break;
			case ItemType.BlackMarker:
				InventoryManager.Instance.inventoryData.isGetBlackMarker = true;
				break;
			case ItemType.GravityMarker:
				InventoryManager.Instance.inventoryData.isGetGravityMarker = true;
				break;
			case ItemType.RubberMarker:
				InventoryManager.Instance.inventoryData.isGetRubberMarker = true;
				break;
			case ItemType.BlackMarkerOne:
				InventoryManager.Instance.inventoryData.isGetUpgradeBlack1 = true;
				break;
			case ItemType.BlackMarkerTwo:
				InventoryManager.Instance.inventoryData.isGetUpgradeBlack2 = true;
				break;
			case ItemType.BlackMarkerThree:
				InventoryManager.Instance.inventoryData.isGetUpgradeBlack3 = true;
				break;
			case ItemType.GravityMarkerOne:
				InventoryManager.Instance.inventoryData.isGetUpgradeGravity1 = true;
				break;
			case ItemType.GravityMarkerTwo:
				InventoryManager.Instance.inventoryData.isGetUpgradeGravity2 = true;
				break;
			case ItemType.GravityMarkerThree:
				InventoryManager.Instance.inventoryData.isGetUpgradeGravity3 = true;
				break;
			case ItemType.RubberMarkerOne:
				InventoryManager.Instance.inventoryData.isGetUpgradeRubber1 = true;
				break;
			case ItemType.RubberMarkerTwo:
				InventoryManager.Instance.inventoryData.isGetUpgradeRubber2 = true;
				break;
			case ItemType.RubberMarkerThree:
				InventoryManager.Instance.inventoryData.isGetUpgradeRubber3 = true;
				break;
		}
		GameEventManager.Instance.GetGameEvent("7.GetItem").Raise();
	}

	private bool Check()
	{
		switch(itemType)
		{
			case ItemType.None:
				return true;
			case ItemType.Gun:
				return InventoryManager.Instance.inventoryData.isGetGun;
			case ItemType.BigGun:
				return InventoryManager.Instance.inventoryData.isGetBigGun;
			case ItemType.BlackMarker:
				return InventoryManager.Instance.inventoryData.isGetBlackMarker;
			case ItemType.GravityMarker:
				return InventoryManager.Instance.inventoryData.isGetGravityMarker;
			case ItemType.RubberMarker:
				return InventoryManager.Instance.inventoryData.isGetRubberMarker;
			case ItemType.BlackMarkerOne:
				return InventoryManager.Instance.inventoryData.isGetUpgradeBlack1;
			case ItemType.BlackMarkerTwo:
				return InventoryManager.Instance.inventoryData.isGetUpgradeBlack2;
			case ItemType.BlackMarkerThree:
				return InventoryManager.Instance.inventoryData.isGetUpgradeBlack3;
			case ItemType.GravityMarkerOne:
				return InventoryManager.Instance.inventoryData.isGetUpgradeGravity1;
			case ItemType.GravityMarkerTwo:
				return InventoryManager.Instance.inventoryData.isGetUpgradeGravity2;
			case ItemType.GravityMarkerThree:
				return InventoryManager.Instance.inventoryData.isGetUpgradeGravity3;
			case ItemType.RubberMarkerOne:
				return InventoryManager.Instance.inventoryData.isGetUpgradeRubber1;
			case ItemType.RubberMarkerTwo:
				return InventoryManager.Instance.inventoryData.isGetUpgradeRubber2;
			case ItemType.RubberMarkerThree:
				return InventoryManager.Instance.inventoryData.isGetUpgradeRubber3;

		}
		return false;
	}
}
