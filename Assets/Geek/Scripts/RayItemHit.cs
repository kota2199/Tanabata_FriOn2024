using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayItemHit : MonoBehaviour
{
    // メインカメラの参照
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float rayLength = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // カメラの中央からRayを飛ばす
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // Rayがオブジェクトに当たった場合
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                // 当たったオブジェクトの情報を取得
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Hit object: " + hitObject.name);

                if(hitObject.tag == "TrueItemParent")
                {
                    hitObject.GetComponent<StartGenerator>().isFounded();
                }

                // 必要に応じて、追加の処理をここに記述
            }
        }
    }
}
