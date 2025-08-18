using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class EnemyChaseState : IEnemyState
{
    public Enemy owner { get; }
    public EnemyChaseState(Enemy enemy)
    {
        owner = enemy;
    }
    public void Enter()
    {
        //Debug.Log($"{owner.name} → Chase 상태 진입");
        owner.EnableAnimation(EnemyState.Chase);
        owner.SprintSpeed();

    }

    public void Stay()
    {
        if (!owner.CanSeePlayer())
        {
            owner.currentState = EnemyState.Idle;
            return;
        }

        owner.ChaseTarget(); // 플레이어 위치 계속 갱신

        if (owner.IsCloseToPlayer())
        {
            //Debug.Log("change attack");
            owner.currentState = EnemyState.Attack;
            return;
        }
    }


    public void Exit()
    {
        owner.InitSpeed();
        owner.DisableAnimation(EnemyState.Chase);
        
    }

    
}
