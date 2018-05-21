using UnityEngine;
using System.Collections;

//This will not work as a SO is an asset and can not get any information from gameobjects

[CreateAssetMenu(fileName = "PlayerPositionTracker", menuName = "Scriptable Objects", order = 1)]

public class PlayerPositionTrackerScriptableObject : ScriptableObject {
    
    public vector3 playerPosition;

}
