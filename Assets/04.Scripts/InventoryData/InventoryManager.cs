using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

public class InventoryManager : MonoSingleton<InventoryManager>
{
	public InventoryData inventoryData = new InventoryData(); 
}
