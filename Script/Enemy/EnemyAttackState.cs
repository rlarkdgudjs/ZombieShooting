using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public Enemy owner { get; }
    public EnemyAttackState(Enemy enemy)
    {
        owner = enemy;
    }
    public void Enter()
    {
        //Debug.Log($"{owner.name} �� Attack ���� ����");
        owner.Attack();
        
        
    }
    public void Stay()
    {
        
    }
    public void Exit()
    {
        //Debug.Log($"{owner.name} �� Attack ���� ����");
    }


}
