using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Image icon;
    public SlotData slotdata = null;
    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public GameObject item;
    private int originalSiblingIndex;
    private void Awake()
    {
        slotdata = new SlotData();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        if (slotdata != null)
        {
            ChangeIcon();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotdata == null || slotdata.IsEmpty)
            return;

        item.transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        item.transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        item.transform.SetParent(this.transform);
        item.transform.localPosition = Vector3.zero;
        

    }

    public void OnDrop(PointerEventData eventData)
    {

        
        Slot dragged = eventData.pointerDrag?.GetComponent<Slot>();
        
        if (!dragged.slotdata.IsEmpty  && dragged != this)
        {
            // 데이터 스왑
            SlotData temp = slotdata;
            slotdata = dragged.slotdata;
            dragged.slotdata = temp;

            // UI 갱신
            this.GetComponent<SlotUI>().SetSlot(slotdata);
            dragged.GetComponent<SlotUI>().SetSlot(dragged.slotdata);
        }
    }

    public void ChangeIcon()
    {
        if (slotdata.item != null)
        {
            icon.sprite = slotdata.item.itemIcon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}


