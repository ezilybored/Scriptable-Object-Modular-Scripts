using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NPCItemTracker", menuName = "Scriptable Objects", order = 1)]

public class NPCItemTrackerScriptableObject : ScriptableObject {
    
    public InventoryItemScriptableObject itemOne;
    
    public InventoryItemScriptableObject itemTwo;
    
    public InventoryItemScriptableObject itemThree;

}
