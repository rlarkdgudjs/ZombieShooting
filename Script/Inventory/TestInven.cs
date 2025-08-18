using UnityEngine;

public class TestInven : MonoBehaviour
{
    public GameObject player;
    public SO_ItemData[] itemDatas;
    public GameObject[] slotPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    int i = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            slotPrefab[i].GetComponent<Slot>().slotdata.item = itemDatas[i];
            slotPrefab[i].GetComponent<Slot>().ChangeIcon();
            i++;
        }
        
    }
}
