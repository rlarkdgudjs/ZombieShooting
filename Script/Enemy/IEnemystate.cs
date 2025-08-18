using UnityEngine;

public enum EnemyState
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead
}
public interface IEnemyState
{
    void Enter();
    void Stay();
    void Exit();
}
