using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Scriptable Objects", order = 1)]

public class PlayerHealthScriptableObject : ScriptableObject {
    
    public float playerMaxHealth;
    public float playerHealth;

}
