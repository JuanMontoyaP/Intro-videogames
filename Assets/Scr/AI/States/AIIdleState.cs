using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Enter(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        Debug.LogError("On idle state");
    }

    public void Exit(AIAgent agent)
    {

    }
}
