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
        Vector3 directionToTarget = agent.Target.position - agent.transform.position;

        if (_timerToRefresh < 0f)
        {
            agent.MovableAgent.GoTo(agent.Target.position - directionToTarget.normalized * 0.75f);
            _timerToRefresh = agent.AIConfig.pathfindingRefreshTime;
        }

        if (directionToTarget.magnitude < agent.AIConfig.attackRange)
        {
            agent.StateMachine.ChangeState(AIStateID.Attack);
        }
    }

    public void Exit(AIAgent agent)
    {
    }
}
