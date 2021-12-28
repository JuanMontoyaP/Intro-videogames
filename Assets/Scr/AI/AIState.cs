public enum AIStateID
{
    Idle = 0,
    ChaseTarget = 1,
    Attack = 2,
}

public interface AIState
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}