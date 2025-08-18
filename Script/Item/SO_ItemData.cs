using UnityEngine;


[CreateAssetMenu(fileName = "SO_ItemData", menuName = "Scriptable Objects/SO_Item")]
public class SO_ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(3, 5)] public string itemDescription;
    public Sprite itemIcon;
    public int itemID;
    public ItemType itemType;
}

