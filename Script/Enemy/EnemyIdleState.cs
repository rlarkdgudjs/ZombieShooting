using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float timer = 0f;
    public Enemy owner { get; }
    public EnemyIdleState(Enemy enemy)
    {
        owner = enemy;
    }

    public void Enter()
    {
        //Debug.Log($"{owner.name} → Idle 상태 진입");
        owner.StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        float waitTime = Random.Range(2f, 4f);
        timer = 0f;

        while (timer < waitTime)
        {
            if (owner.CanSeePlayer())
            {
                owner.currentState = EnemyState.Chase;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null; 
        }
        owner.currentState = EnemyState.Move;
    }

    public void Stay()
    {
       
    }

    public void Exit()
    {
        timer = 0f;
    }
}
