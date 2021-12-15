using System;
using UnityEngine;
using UnityEngine.AI;

public enum AgentState
{
    Idle,
    Moving,
}

public class MovableAgent : MonoBehaviour
{
    public AgentState State {get; private set; }

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_TargetPosition;
    private Action m_OnArrive;
    
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (State == AgentState.Moving)
        {
            var distance = Vector3.Distance(transform.position, m_TargetPosition);
            if (distance <= m_NavMeshAgent.stoppingDistance)
            {
                State = AgentState.Idle;
                m_OnArrive();
                m_OnArrive = null;
            }
        }
    }

    public void GoTo(Vector3 position, Action onArrive = null)
    {
        m_OnArrive = onArrive;
        m_TargetPosition = position;
        State = AgentState.Moving;
        m_NavMeshAgent.SetDestination(position);
    }
}
