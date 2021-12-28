using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] 

public class AIConfig : ScriptableObject
{
    public float detectRange = 5f;
    public float pathfindingRefreshTime = 1f;
    public float attackRange = 1f;
    public float attackDuration = 1f;
}
