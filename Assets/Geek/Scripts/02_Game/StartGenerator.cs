using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject star;

    [SerializeField]
    private Vector3 offset;

    public void isFounded()
    {
        Vector3 genePos = this.transform.position + offset;
        Instantiate(star, genePos, Quaternion.identity);
    }
}
