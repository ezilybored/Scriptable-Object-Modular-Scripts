using UnityEngine;
using System.Collections;

//This is the bit that replaces the code found in MakeScriptableObject
[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
//				Class name				Derives from ScriptableObject
public class MyScriptableObjectClass : ScriptableObject {
	//A string
	public string objectName = "New MyScriptableObject";
	//Should the colour be picked randomly?
	public bool colorIsRandom = false;
	//A set colour for the object whatever it may be
	public Color thisColor = Color.white;
	//A vector tracking spawn points.
	public Vector3[] spawnPoints;

	//These are all just random bits of data for this scriptable object to store
}
