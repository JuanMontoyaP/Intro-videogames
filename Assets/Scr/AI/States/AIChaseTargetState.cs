using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseTargetState : AIState
{
    private float _timerToRefresh = 0f;
    public AIStateID GetID()
    {
        return AIStateID.ChaseTarget;
    }

    public void Enter(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        if (agent.Target == null)
        {
           return;
        }

        _timerToRefresh -= Time.deltaTime;
        if (_timerToRefresh < 0f)
        {
            Vector3 directionToTarget = (agent.Target.position - agent.transform.position).normalized;
            agent.MovableAgent.GoTo(agent.Target.position - directionToTarget * 1.0f);
            _timerToRefresh = agent.AIConfig.pathfindingRefreshTime;
        }
    }

    public void Exit(AIAgent agent)
    {

    }
}
