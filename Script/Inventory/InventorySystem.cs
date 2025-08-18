using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public GameObject[] slots;
    public bool IsFull = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryGetItem(SO_ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i].GetComponent<Slot>();
            if ( i == slots.Length-1 && !slot.slotdata.IsEmpty)
            {
                IsFull = true;
                return false;
            }
            
            if (slot.slotdata == null || slot.slotdata.IsEmpty)
            {
                slot.slotdata.item = item;
                slot.ChangeIcon();

                return true;
            }
        }
        return false;
    }
}
