using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private MovableAgent _movableAgent;
    private AIStateMachine _stateMachine;

    public Transform Target => _player;
    public MovableAgent MovableAgent => _movableAgent; 

    void Start()
    {
        _movableAgent = GetComponent<MovableAgent>();

        _stateMachine = new AIStateMachine(this);

        _stateMachine.AddState(new AIIdleState());
        _stateMachine.AddState(new AIChaseTargetState());
    }

    void Update()
    {
        _stateMachine.Update();
    }
}
