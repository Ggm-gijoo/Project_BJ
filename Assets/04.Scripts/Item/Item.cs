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
				break;
			case ItemType.BlackMarkerTwo:
				break;
			case ItemType.BlackMarkerThree:
				break;
			case ItemType.GravityMarkerOne:
				break;
			case ItemType.GravityMarkerTwo:
				break;
			case ItemType.GravityMarkerThree:
				break;
			case ItemType.RubberMarkerOne:
				break;
			case ItemType.RubberMarkerTwo:
				break;
			case ItemType.RubberMarkerThree:
				break;
		}
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
				return false;
			case ItemType.BlackMarkerTwo:
				return false;
			case ItemType.BlackMarkerThree:
				return false;
			case ItemType.GravityMarkerOne:
				return false;
			case ItemType.GravityMarkerTwo:
				return false;
			case ItemType.GravityMarkerThree:
				return false;
			case ItemType.RubberMarkerOne:
				return false;
			case ItemType.RubberMarkerTwo:
				return false;
			case ItemType.RubberMarkerThree:
				return false;

		}
		return false;
	}
}
