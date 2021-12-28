using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    private AIStateMachine _stateMachine;

    void Start()
    {
        _stateMachine = new AIStateMachine(this);
        _stateMachine.AddState(new AIIdleState());
        // _stateMachine.AddState(new AIChaseState());
    }

    void Update()
    {
        _stateMachine.Update();
    }
}
