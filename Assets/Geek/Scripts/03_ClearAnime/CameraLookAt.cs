using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform target; // 注視する対象のTransform
    public Vector3 positionOffset = Vector3.zero; // 位置のオフセット
    public Vector3 lookAtOffset = Vector3.zero; // 向きのオフセット

    void LateUpdate()
    {
        if (target != null)
        {
            // カメラの新しい位置を計算（y座標はターゲットと同じ）
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, target.position.z) + positionOffset;
            transform.position = newPosition;

            // ターゲットの位置に向きのオフセットを加算して注視点を計算
            Vector3 targetPosition = target.position + lookAtOffset;

            // カメラをターゲットの方向に回転
            transform.LookAt(targetPosition);
        }
    }
}
