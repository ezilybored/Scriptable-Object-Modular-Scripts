using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InventoryItemEditor : EditorWindow {

	//references the Inventory
	public InventoryItemList inventoryItemList;
	//Added by me so that I can create a new Inventory item scripatble object when an item is added
	public InventoryItem inventoryItem;
	private int viewIndex = 1;

	//Allows the creation of a new Inventory Item editor for the inspector
	[MenuItem ("Window/Inventory Item Editor %#e")]
	static void  Init () 
	{
		//Creates and gets a new floating window oftype InventoryItemEditor
		EditorWindow.GetWindow (typeof (InventoryItemEditor));
	}

	void  OnEnable () {
		//Finding the object path
		//Does the Editor have the key "ObjectPath"
		if(EditorPrefs.HasKey("ObjectPath")) 
		{
			//Sets a string called objectPath as the return from EditorPrefs.GetString 
			//which should be the same as the key "ObjectPath"
			string objectPath = EditorPrefs.GetString("ObjectPath");
			//Returns the first asset object of type InventoryItemList at given path objectPath
			//sets as inventoryItemList
			inventoryItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(InventoryItemList)) as InventoryItemList;
		}

	}

	//OnGUI is called for rendering and handling GUI events. Can be called several times in a script
	void  OnGUI () {
		//Begin a Horizontal control group.
		//All controls rendered inside this element will be placed horizontally next to each other.
		GUILayout.BeginHorizontal ();
		//Labels have no user interaction, do not catch mouse clicks and are always rendered in normal style.
		//This is a label description shown when the label is hovered over. It is bold
		GUILayout.Label ("Inventory Item Editor", EditorStyles.boldLabel);
		//If there is a InventoryItemList (not null)
		if (inventoryItemList != null) 
		{
			//If the button pressed is "Show Item List"
			if (GUILayout.Button("Show Item List")) 
			{
				//Brings the project window to the front and focuses it.
				EditorUtility.FocusProjectWindow();
				//Returns the actual object selection. Sets it as inventoryItemList
				//Essentially selects the currently active Inventory list
				Selection.activeObject = inventoryItemList;
			}
		}
		//If the button pressed is "Show Item List"
		if (GUILayout.Button("Open Item List")) 
		{
			//Runs the method OpenItemList defined below
			OpenItemList();
		}
		//If the button pressed is "New Item List"
		if (GUILayout.Button("New Item List")) 
		{
			//Brings the project window to the front and focuses it.
			EditorUtility.FocusProjectWindow();
			//Returns the actual object selection. Sets it as inventoryItemList.
			//Essentially selects the currently active Inventory list
			Selection.activeObject = inventoryItemList;
		}
		
		//Ends the Horizontal group
		GUILayout.EndHorizontal ();

		//If the inventoryItemList is null
		if (inventoryItemList == null) 
		{
			//Begin a Horizontal control group.
			GUILayout.BeginHorizontal ();
			//Creates a space in the GUI 10 pixels big
			GUILayout.Space(10);
			//If the button pressed is "Show Item List"
			//GUILayout.ExpandWidth(false) limits the expansion of the editor window
			if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false))) 
			{
				//Runs the method CreateNewItemList described below
				CreateNewItemList();
			}
			//If the button pressed is "Open Existing Item List"
			if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
			{
				//Runs the method OpenItemList described below
				OpenItemList();
			}
			//Ends the Horizontal group
			GUILayout.EndHorizontal ();
		}

		//Insert a space in the current layout group. The space is 20 pixels big
		GUILayout.Space(20);

		//If the inventoryItemList is not null
		if (inventoryItemList != null) 
		{
			//Begin a Horizontal control group.
			GUILayout.BeginHorizontal ();

			//Insert a space in the current layout group. The space is 10 pixels big
			GUILayout.Space(10);

			//If the button pressed is "Prev"
			if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
			{
				//If the viewIndex variable is bigger than 1
				if (viewIndex > 1)
					//decrease the value of viewIndex by 1
					viewIndex --;
			}
			//Insert a space in the current layout group. The space is 5 pixels
			GUILayout.Space(5);
			//if the button clicked is "Next", do not expand the window
			if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
			{
				//If the viewIndex variable is less than the number of items in the InventoryList
				if (viewIndex < inventoryItemList.itemList.Count) 
				{
					//Increment the viewIndex by +1
					viewIndex ++;
				}
			}

			//Insert a space in the current layout group. The space is 60 pixels 
			GUILayout.Space(60);

			//If the button clicked is "Add Item", do not expand window
			if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
			{
				//Run the method AddItem
				AddItem();
			}
			//If the button clicked is "Delete Item", do not expand window
			if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
			{
				//Run the method DeleteItem then decrement viewIndex by -1
				DeleteItem(viewIndex - 1);
			}
			//Ends the Horizontal group
			GUILayout.EndHorizontal ();
			
			//If the inventoryItemList.itemList is null
			//Is the number of inventoryItemLists 0?
			if (inventoryItemList.itemList == null)
				Debug.Log("wtf");
			
			//If the inventoryItemList.itemList is more than 0
			//Essentially, if there is an Inventory already created
			if (inventoryItemList.itemList.Count > 0) 
			{
				//Begin a Horizontal control group.
				GUILayout.BeginHorizontal ();
				//Mathf.Clamp clamps between a maximum and minimum value (value, min, max)
				//EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)) gives the value to clamp
				//EditorGUILayout.IntField makes a text field that allows the entry of integers
				//"Current Item" is the label, viewIndex is the value, GUILayout.ExpandWidth(false)) limits the expansion of the box
				//1 is the minimum value for the clamp
				//inventoryItemList.itemList.Count is the maximum. Limits it within the value of the number of Items in the Inventory
				viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemList.Count);
				//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count); Not sure why this line is commented out
				//EditorGUILayout.LabelField makes a label field
				//Label field contains "of" then the size of the inventoryItemList the "items"
				//Essentially states the size of the Inventory
				EditorGUILayout.LabelField ("of   " +  inventoryItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
				//Ends the horizontal group
				GUILayout.EndHorizontal ();
				
				
				//Creates a box to enter the name of the Item
				inventoryItemList.itemList[viewIndex-1].itemName = EditorGUILayout.TextField ("Item Name", inventoryItemList.itemList[viewIndex-1].itemName as string);
				//Creates a box to add an icon for the Item
				inventoryItemList.itemList[viewIndex-1].itemIcon = EditorGUILayout.ObjectField ("Item Icon", inventoryItemList.itemList[viewIndex-1].itemIcon, typeof (Texture2D), false) as Texture2D;
				//Creates a box to enter a rigidbody game object for the Item
				inventoryItemList.itemList[viewIndex-1].itemObject = EditorGUILayout.ObjectField ("Item Object", inventoryItemList.itemList[viewIndex-1].itemObject, typeof (Rigidbody), false) as Rigidbody;
				
				//These can be added to and or edited depending on the fields that the Item requires
				
				//Creates a space of 20 pixels
				GUILayout.Space(10);

				//Begins a horizontal group
				GUILayout.BeginHorizontal ();
				//Creates a check box for Unique object property
				inventoryItemList.itemList[viewIndex-1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex-1].isUnique, GUILayout.ExpandWidth(false));
				//Creates a check box for Indestructible object property
				inventoryItemList.itemList[viewIndex-1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex-1].isIndestructible,  GUILayout.ExpandWidth(false));
				//Creates a check box for QuestItem object property
				inventoryItemList.itemList[viewIndex-1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex-1].isQuestItem,  GUILayout.ExpandWidth(false));
				//Ends the horizontal group
				GUILayout.EndHorizontal ();

				//These can be added to and or edited depending on the fields that the Item requires
				
				//Creates a space of 10 pixels
				GUILayout.Space(10);

				//Begins a horizontal group
				GUILayout.BeginHorizontal ();
				//Creates a check box for stackable object property
				inventoryItemList.itemList[viewIndex-1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex-1].isStackable , GUILayout.ExpandWidth(false));
				//Creates a check box for Destory on use object property
				inventoryItemList.itemList[viewIndex-1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex-1].destroyOnUse,  GUILayout.ExpandWidth(false));
				//Creates a check box for Encumberance object property
				inventoryItemList.itemList[viewIndex-1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex-1].encumbranceValue,  GUILayout.ExpandWidth(false));
				//ends the horizontal group
				GUILayout.EndHorizontal ();

				//Creates a space of 10 pixels
				GUILayout.Space(10);

			} 
			//If there are no Items in the Inventory
			else 
			{
				//Creates a lable stating that the Inventory is empty
				GUILayout.Label ("This Inventory List is Empty.");
			}
		}
		//If anything about the Inventory is changed
		//GUI.changed Returns true if any controls changed the value of the input data
		if (GUI.changed) 
		{
			//EditorUtility.SetDirty : Marks target object as dirty. (Only suitable for non-scene objects).
			//Item to be marked is inventoryItemList
			//Dirty is data that has not been updated and needs to be updated
			EditorUtility.SetDirty(inventoryItemList);
		}
	}

	void CreateNewItemList () 
	{
		// There is no overwrite protection here!
		// There is No "Are you sure you want to overwrite your existing object?" if it exists.
		// This should probably get a string from the user to create a new name and pass it ...
		viewIndex = 1;
		inventoryItemList = CreateInventoryItemList.Create();
		if (inventoryItemList) 
		{
			inventoryItemList.itemList = new List<InventoryItem>();
			string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
			EditorPrefs.SetString("ObjectPath", relPath);
		}
	}

	void OpenItemList () 
	{
		string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
		if (absPath.StartsWith(Application.dataPath)) 
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			inventoryItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(InventoryItemList)) as InventoryItemList;
			if (inventoryItemList.itemList == null)
				inventoryItemList.itemList = new List<InventoryItem>();
			if (inventoryItemList) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
	}

	void AddItem () 
	{
		//Creates a new Scriptable object item called inventoryItem
		inventoryItem = CreateInventoryItem.Create();
		//Sets the name of the Scriptable object item
		inventoryItem.itemName = "New Item";
		//Adds the item to the inventory
		inventoryItemList.itemList.Add (inventoryItem);
		//Increases the count of the inventory
		viewIndex = inventoryItemList.itemList.Count;
	}

	void DeleteItem (int index) 
	{
		//At a future date add the script for removing the scriptabel object from the inventory here
		//Currently deleting only removes the item from the list.
		//This may be a good thing though.
		inventoryItemList.itemList.RemoveAt (index);
	}
}
