using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Image icon;
    //public TextMeshProUGUI quantityText;

    private SO_ItemData currentitem;

    public void SetSlot(SO_ItemData item)
    {
        currentitem = item;

        if (item == null)
        {
            icon.enabled = false;
            //quantityText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = item.itemIcon;

            //quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }

        if (item == null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        else
            ChangeIcon(item.itemIcon);
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
