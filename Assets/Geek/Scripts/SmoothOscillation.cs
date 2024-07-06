using UnityEngine;

public class SmoothOscillation : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.up; // 移動方向（インスペクターで設定可能）
    public float speed = 1f; // 移動速度（インスペクターで設定可能）
    public float distance = 2f; // 移動距離（インスペクターで設定可能）

    private Vector3 startPosition; // 初期位置
    private float currentTime = 0f; // 経過時間

    void Start()
    {
        startPosition = transform.position; // 初期位置を保存
    }

    void Update()
    {
        // Sin関数を使用してスムーズな往復運動を実現
        currentTime += Time.deltaTime * speed;
        float offset = Mathf.Sin(currentTime) * distance;

        // 新しい位置を計算
        Vector3 newPosition = startPosition + moveDirection.normalized * offset;

        // オブジェクトの位置を更新
        transform.position = newPosition;
    }
}
