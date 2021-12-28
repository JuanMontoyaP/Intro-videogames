using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private AIConfig _aiConfig;

    private MovableAgent _movableAgent;
    private AIStateMachine _stateMachine;

    public Transform Target => _player;
    public AIConfig AIConfig => _aiConfig;
    public MovableAgent MovableAgent => _movableAgent; 
    public AIStateMachine StateMachine => _stateMachine;

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
