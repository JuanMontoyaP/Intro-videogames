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
        if (m_Target != null)
        {
            m_MovableAgent.GoTo(m_Target.position);
        }
    }
}
