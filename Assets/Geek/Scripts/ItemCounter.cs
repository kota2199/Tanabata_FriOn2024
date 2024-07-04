using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public int itemCount;
    
    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            AddItemCount();
            Destroy(other.gameObject);
        }
    }

    public void AddItemCount()
    {
        itemCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {

    }
}
