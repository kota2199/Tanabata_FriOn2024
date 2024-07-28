using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f); // 回転速度 (x, y, z)軸の速度

    void Update()
    {
        // 毎フレームrotationSpeedの値だけ回転
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
