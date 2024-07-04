using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalItem : MonoBehaviour
{
    [SerializeField]
    private Image[] itemImages;

    [SerializeField]
    private Sprite activeStar, negativeStar;

    public int additionalItemCounter;

    // Start is called before the first frame update
    void Start()
    {
        additionalItemCounter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AdditionalItem")
        {
            AudioController.instance.PlaySE(1);
            additionalItemCounter++;
            itemImages[additionalItemCounter - 1].sprite = activeStar;
            Destroy(other.gameObject);
        }
    }
}
