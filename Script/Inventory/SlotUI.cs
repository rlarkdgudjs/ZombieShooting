using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    //public TextMeshProUGUI quantityText;

    private SlotData currentSlot;

    public void SetSlot(SlotData slot)
    {
        currentSlot = slot;
        
        if (slot.IsEmpty)
        {
            icon.enabled = false;
            //quantityText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = slot.item.itemIcon;

            //quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }

        if (slot.item == null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        else
            ChangeIcon(slot.item.itemIcon);
    }
    public void ChangeIcon(Sprite imgae)
    {
        if (imgae != null)
        {
            icon.sprite = imgae;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
