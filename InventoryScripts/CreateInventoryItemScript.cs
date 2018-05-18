using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateInventoryItemScript {
	//In this case this part isn't necessarily needed
	[MenuItem("Assets/Create/Inventory Item")]
  
	public static InventoryItem  Create()
	{
		InventoryItem asset = ScriptableObject.CreateInstance<InventoryItem>();

		AssetDatabase.CreateAsset(asset, "Assets/InventoryItem.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
  
  //This script is not a scriptable object but is used with the custom editor to make new Inventory Items
}
