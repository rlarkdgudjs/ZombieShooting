using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public float pickupRadius = 2f;
    public LayerMask itemLayer;
    public KeyCode pickupKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            TryPickupNearbyItems();
        }
    }

    void TryPickupNearbyItems()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius, itemLayer);

        foreach (Collider col in colliders)
        {
            ItemObject itemObj = col.GetComponent<ItemObject>();
            if (itemObj != null)
            {
                if (InventorySystem.Instance.TryGetItem(itemObj.itemData))
                {
                    Destroy(col.gameObject);
                }

                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
