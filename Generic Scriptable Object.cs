using UnityEngine;
using System.Collections;

//This is the bit that replaces the code found in MakeScriptableObject
[CreateAssetMenu(fileName = "Filename goes here", menuName = "This is the name of the menu the SO is found under", order = 1)]
//		Class name		Derives from ScriptableObject
public class MyScriptableObjectClassName : ScriptableObject {

	//The data or functions for the SO go in here
	//E.g
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
