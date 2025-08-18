using UnityEngine;
//쓰이지않음
public class CItem : MonoBehaviour
{
    
    public SO_ItemData itemData;
    // Weapon specific properties
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    public void SetItemData(SO_ItemData item)
    {
        itemData = item;
    }
}
