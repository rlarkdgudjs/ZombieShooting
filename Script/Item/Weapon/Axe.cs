using UnityEngine;

public class Axe : MonoBehaviour,IWeapon
{
    public GameObject player;
    public float attackRange = 1.5f;
    public int damage = 20;
    public LayerMask enemyLayers;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PerformAttack()
    {        
        Vector3 pos = player.transform.position + player.transform.forward;
        pos.y += 1f;
        Collider[] hitEnemies = Physics.OverlapBox(pos, new Vector3(0.5f, 0.1f, 0.5f), Quaternion.identity, enemyLayers);
    
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy == null) continue;
            Debug.Log($"{enemy.name}"+enemy.gameObject.layer);
            if (enemy.gameObject.layer != 11) continue;
            enemy.gameObject.GetComponentInParent<Enemy>().currentState = EnemyState.Dead;
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Vector3 pos = player.transform.position + player.transform.forward;
        Vector3 halfExtents = new Vector3(0.5f, 0.1f, 0.5f);
        pos.y += 1f;
        Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f);
    }
}
