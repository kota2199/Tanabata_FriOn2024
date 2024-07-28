using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTowardPlayer : MonoBehaviour
{
    public float speed = 5f; // 移動速度
    public float arcHeight = 2f; // 弧の高さ
    public float waitTime = 2f; // 移動開始までの待機時間

    private Transform target; // ターゲット
    private Vector3 startPos; // 開始位置
    private float time = 0f; // 時間の経過
    private bool isMoving = false; // 移動開始フラグ

    void Start()
    {
        // Playerタグがついているオブジェクトを見つける
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        startPos = transform.position;

        // コルーチンを開始して指定した秒数待機する
        StartCoroutine(WaitAndStartMoving(waitTime));
    }

    void Update()
    {
        if (!isMoving || target == null) return;

        // 目的地までの割合
        time += Time.deltaTime * speed;

        // 位置を線形補間で計算
        Vector3 nextPos = Vector3.Lerp(startPos, target.position, time);

        // 弧を描くようにY座標を調整
        nextPos.y += arcHeight * Mathf.Sin(Mathf.Clamp01(time) * Mathf.PI);

        // オブジェクトの位置を更新
        transform.position = nextPos;

        // ターゲットに向かって向きを更新
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        // 目標に到達したら弧をリセット
        if (time >= 1f)
        {
            startPos = transform.position;
            time = 0f;
        }
    }

    private IEnumerator WaitAndStartMoving(float waitTime)
    {
        // 指定した秒数待機
        yield return new WaitForSeconds(waitTime);
        // 移動開始フラグをtrueに設定
        isMoving = true;
    }
}
