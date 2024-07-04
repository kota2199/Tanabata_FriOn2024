using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Playerの状態
public enum StateType
{
    STANDING,
    WALKING,
    RUNNING,
    JUMPING,
}

public interface IPlayerState
{
    // このクラスの状態を取得する
    StateType stateType{ get; }
    // 状態開始時に最初に実行される
    void Entry();
    // フレームごとに実行される
    void Execute();
    // 状態終了時に実行される
    void Exit();
}
