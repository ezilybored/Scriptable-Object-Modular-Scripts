using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerPositionTracker", menuName = "Scriptable Objects", order = 1)]

public class PlayerPositionTrackerScriptableObject : ScriptableObject {
    
    public vector3 playerPosition;

}
