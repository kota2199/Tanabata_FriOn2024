using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject star;

    public void isFounded()
    {
        Vector3 genePos = this.transform.position + new Vector3(0, 1, 0);
        Instantiate(star, genePos, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
