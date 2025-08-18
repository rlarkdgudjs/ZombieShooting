using UnityEngine;


public class EnemyMoveState : IEnemyState
{
    public Enemy owner { get; }
    private float timer = 0f;
    private float distance = 0f;
    public EnemyMoveState(Enemy enemy)
    {
        owner = enemy;
    }

    public void Enter()
    {
        owner.EnableAnimation(EnemyState.Move);
        owner.MoveToRandomPosition();
        //Debug.Log($"{owner.name} → Move 상태 진입");
    }

    public void Stay()
    {
        timer += Time.deltaTime;
        if (timer >= owner.checkInterval)
        {
            timer = 0f;
            if (owner.CanSeePlayer())
            {
                owner.currentState = EnemyState.Chase;
                return;
            }
        }
       
        
        if (owner.IsArrived())
        {
            owner.currentState = EnemyState.Idle;
        }
    }

    public void Exit()
    {
        distance = 0f;
        timer = 0f;
        owner.DisableAnimation(EnemyState.Move);
        //Debug.Log($"{owner.name} → Move 상태 종료");
    }
}
