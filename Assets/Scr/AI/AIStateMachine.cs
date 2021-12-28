using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private AIState[] _states;
    private AIAgent _agent;
    private AIStateID _currentStateID = AIStateID.ChaseTarget;

    public AIStateMachine(AIAgent agent)
    {
        _agent = agent;
        int totalStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        _states = new AIState[totalStates];
    }

    public void AddState(AIState state)
    {
        AIStateID id = state.GetID();
        int index = (int) id;
        _states[index] = state;
        // _states[(ind) state.GetID()] = state;
    }

    public AIState GetState(AIStateID id)
    {
        return _states[(int) id];
    }

    public void Update()
    {
        AIState currentState = GetState(_currentStateID);
        if (currentState != null)
        {
            currentState.Update(_agent);
        }
        // currentState?.Update(_agent);
        // GetState(_currentStateID)?.Update(_agent);
    }
}
