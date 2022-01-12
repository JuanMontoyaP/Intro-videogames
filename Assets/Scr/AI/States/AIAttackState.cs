using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    private float _timer = 0;

    public AIStateID GetID()
    {
        return AIStateID.Attack;
    }

    public void Enter(AIAgent agent)
    {
        if (agent.Target.TryGetComponent(out IDamageable target))
        {
            target.TakeHit(1, agent.Target.position, agent.Target.forward);
        }
        
        _timer = agent.AIConfig.attackDuration;
    }

    public void Update(AIAgent agent)
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            agent.StateMachine.ChangeState(AIStateID.Idle);
        }
    }

    public void Exit(AIAgent agent)
    {

    }
}
