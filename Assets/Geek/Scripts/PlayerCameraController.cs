using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float rotateSpeed = 5f; // 回転速度
    public float heightOffset = 1.5f; // カメラの高さオフセット
    public float distance = 5f; // プレイヤーからカメラまでの距離
    public float maxVerticalAngle = 60f; // 上下回転の最大角度
    public float minDistance = 1f; // 障害物がある場合の最小距離

    private float currentRotationY = 0f;
    private float currentRotationX = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // マウスの動きに基づいて回転角度を更新
        currentRotationY += mouseX * rotateSpeed;
        currentRotationX -= mouseY * rotateSpeed;

        // 上下回転の制限
        currentRotationX = Mathf.Clamp(currentRotationX, -maxVerticalAngle, maxVerticalAngle);

        // 回転をQuaternionに変換
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);

        // カメラの位置をプレイヤーの位置とオフセットに基づいて計算
        Vector3 offset = new Vector3(0, heightOffset, -distance);
        Vector3 targetPosition = player.position + rotation * offset;

        // カメラとプレイヤーの間に障害物がないかRaycastでチェック
        RaycastHit hit;
        if (Physics.Raycast(player.position + new Vector3(0, heightOffset, 0), targetPosition - (player.position + new Vector3(0, heightOffset, 0)), out hit, distance))
        {
            // 障害物がある場合、カメラの位置を調整
            float adjustedDistance = Vector3.Distance(player.position + new Vector3(0, heightOffset, 0), hit.point) - 0.5f;
            targetPosition = player.position + rotation * new Vector3(0, heightOffset, -Mathf.Clamp(adjustedDistance, minDistance, distance));
        }

        // カメラの位置と回転を更新
        transform.position = targetPosition;
        transform.LookAt(player.position + new Vector3(0, heightOffset, 0));
    }
}
