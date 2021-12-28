using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private MovableAgent m_MovableAgent;
 
    void Start()
    {
        m_MovableAgent = GetComponent<MovableAgent>();
    }

    void Update()
    {
        if (m_Target != null && Vector3.Distance(m_Target.position, m_MovableAgent.TargetPosition) > 0.5f)
        {
            m_MovableAgent.GoTo(m_Target.position, OnArrive);
        }
    }

    private void OnArrive()
    {
        Debug.Log("Arrived");
    }
}
