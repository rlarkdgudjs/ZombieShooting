using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    public Enemy owner { get; }
    public EnemyDeadState(Enemy enemy)
    {
        owner = enemy;
    }
    public void Enter()
    {
        
        
        owner.Death();
    }
    public void Stay()
    {

    }
    public void Exit()
    {
        
    }

}
