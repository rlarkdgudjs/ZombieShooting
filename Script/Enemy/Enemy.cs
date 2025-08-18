using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameObject targetObject;
    public Vector3 targetPosition;

    public float moveSpeed = 3.5f;
    public float sprintSpeed = 5f;
    public float checkInterval = 0.3f;
    public float attackDistance = 1f;
    public float hitRange = 0.7f;
    public float attackDelay = 1f;
    public float detectionRadius = 10f;
    public float moveRange = 10f;

    public Animator animator;
    public IEnemyState[] states;
    private EnemyState _currentState;
    private NavMeshAgent agent;

    public float updateInterval = 0.3f; // 추적 주기
    private float updateTimer = 0f;
    public EnemyState currentState
    {
        get => _currentState;
        set
        {

            states[(int)_currentState].Exit();
            _currentState = value;
            states[(int)_currentState].Enter();

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        states = new IEnemyState[System.Enum.GetValues(typeof(EnemyState)).Length];
        states[(int)EnemyState.Idle] = new EnemyIdleState(this);
        states[(int)EnemyState.Move] = new EnemyMoveState(this);
        states[(int)EnemyState.Chase] = new EnemyChaseState(this);
        states[(int)EnemyState.Attack] = new EnemyAttackState(this);
        states[(int)EnemyState.Dead] = new EnemyDeadState(this);
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

    }
    void Start()
    {
        states[(int)currentState].Enter();
    }

    // Update is called once per frame
    void Update()
    {
        states[(int)currentState].Stay();
    }
    public bool CanSeePlayer()
    {
        
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);
        if (distance < detectionRadius) 
        {
            targetObject = player;
            return true; 
        }
        return false;
    }

    public void MoveToRandomPosition()
    {
        if (TryGetRandomNavMeshPosition(transform.position, moveRange, 10, out Vector3 destination))
        {
            targetPosition = destination;
            agent.SetDestination(destination);
        }
        else
        {
            //Debug.LogWarning($"{name} : 유효한 위치를 찾지 못했습니다.");
        }
    }
    public bool IsArrived()
    {
        if(agent.remainingDistance < 0.2f)
        {
            return true;
        }
        return false;
    }

    public bool IsCloseToPlayer()
    {
        //Debug.Log($"Distance to player: {Vector3.Distance(transform.position, player.transform.position)}");
        return Vector3.Distance(transform.position, player.transform.position) <= hitRange;
    }

    private bool TryGetRandomNavMeshPosition(Vector3 center, float radius, int maxAttempts, out Vector3 result)
    {
        

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * radius;
            Vector3 samplePos = center + randomOffset;

            if (NavMesh.SamplePosition(samplePos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = center;
        return false;
    }
    public void ChaseTarget()
    {
        updateTimer += Time.deltaTime;
        
        if (updateTimer >= updateInterval)
        {
            //Debug.Log(updateTimer);
            updateTimer = 0f;
            agent.SetDestination(player.transform.position);
            //Debug.Log($"{name}이(가) player 을(를) 추적합니다.");
        }
    }

    IEnumerator WaitAttacktime(float time)
    {
        animator.SetTrigger("Attack");
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentState = EnemyState.Idle;
    }

    public void Attack()
    {
        //Debug.Log($"{name}이(가) {targetObject.name}을(를) 공격합니다.");
        //transform.LookAt(player.transform.position);
        StartCoroutine(WaitAttacktime(attackDelay));
        
    }
    public void Death()
    {
        agent.isStopped = true;
        animator.SetBool("Death",true);
        Destroy(gameObject,3f);
    }

    public void EnableAnimation(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                animator.SetBool("Idle", true);
                break;
            case EnemyState.Move:
                animator.SetBool("Walk", true);
                break;
            case EnemyState.Chase:
                animator.SetBool("Run", true);
                break;
            
        }

    }
    public void DisableAnimation(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                animator.SetBool("Idle", false);
                break;
            case EnemyState.Move:
                animator.SetBool("Walk", false);
                break;
            case EnemyState.Chase:
                animator.SetBool("Run", false);
                break;
            
        }
    }
    public void SprintSpeed()
    {
        agent.speed = sprintSpeed;
    }
   public void InitSpeed()
    {
        agent.speed = moveSpeed;
    }

    
    
      
    

}
