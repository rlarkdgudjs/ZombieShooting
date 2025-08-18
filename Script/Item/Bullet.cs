using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if(((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            other.gameObject.GetComponentInParent<Enemy>().currentState = EnemyState.Dead;
            // Destroy the bullet when it collides with the player
            Destroy(gameObject);
        }
    }
}

