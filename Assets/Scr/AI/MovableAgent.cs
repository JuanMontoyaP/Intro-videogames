using System;
using UnityEngine;
using UnityEngine.AI;

public enum NavMeshAgentState
{
    Idle,
    Moving,
}

public class MovableAgent : MonoBehaviour
{
    public NavMeshAgentState State
    {
        get
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance && m_NavMeshAgent.velocity.sqrMagnitude == 0)
                return NavMeshAgentState.Idle;

            return NavMeshAgentState.Moving;
        }
    }

    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_TargetPosition;
    private Action m_OnArrive;

    public Vector3 TargetPosition => m_TargetPosition;

    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (m_OnArrive != null)
        {
            if (!m_NavMeshAgent.pathPending && State == NavMeshAgentState.Idle)
            {
                m_OnArrive();
                m_OnArrive = null;
            }
        }
    }

    public void GoTo(Vector3 position, Action onArrive = null)
    {
        m_OnArrive = onArrive;
        m_TargetPosition = position;
        m_NavMeshAgent.SetDestination(position);
    }
}
