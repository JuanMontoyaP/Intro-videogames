using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseTargetState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.ChaseTarget;
    }

    public void Enter(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        if (agent.Target != null /*&& Vector3.Distance(m_Target.position, m_MovableAgent.TargetPosition) > 0.5f*/)
        {
            agent.MovableAgent.GoTo(agent.Target.position);
        }
    }

    public void Exit(AIAgent agent)
    {

    }
}
