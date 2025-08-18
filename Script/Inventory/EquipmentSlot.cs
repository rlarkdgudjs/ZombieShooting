using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour , IDropHandler, IPointerClickHandler
{
    public SO_ItemData itemData;
    public InventorySystem inventorySystem;
    public Transform weaponSlotTransform;
    public Animator animator;
    private GameObject currentWeapon;
    public RuntimeAnimatorController defaultanimator;
    public void Start()
    {
        itemData = new SO_ItemData();
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        Slot dragged = eventData.pointerDrag?.GetComponent<Slot>();

        if (!dragged.slotdata.IsEmpty && dragged != this)
        {
            // 데이터 스왑
            SO_ItemData temp = itemData;
            itemData = dragged.slotdata.item;
            dragged.slotdata.item = temp;

            // UI 갱신
            this.GetComponent<EquipmentUI>().SetSlot(itemData);
            dragged.GetComponent<SlotUI>().SetSlot(dragged.slotdata);
        }
        EquipWeapon(itemData as SO_Weapon);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventorySystem.TryGetItem(itemData);
        CleanSlot();
    }

    void CleanSlot()
    {
        itemData = null;
        this.GetComponent<EquipmentUI>().SetSlot(itemData);
        EquipWeapon(null);
    }

    public void EquipWeapon(SO_Weapon weaponData)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if(weaponData == null)
        {
            
            animator.runtimeAnimatorController = defaultanimator;
            return;
        }
        
        currentWeapon = Instantiate(weaponData.weaponPrefab,weaponSlotTransform);
        currentWeapon.transform.localPosition = weaponData.weaponOffset;
        currentWeapon.transform.localScale = weaponData.weaponSize;
        currentWeapon.transform.localRotation = Quaternion.Euler(weaponData.weaponRotation);
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<InputManager>().SetWeapon(currentWeapon);

        // 무기에 걸맞는 애니메이션 설정
        animator.runtimeAnimatorController = weaponData.weaponAnimator;
        
    }

}
