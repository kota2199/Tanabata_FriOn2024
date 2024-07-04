using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IPlayerState currentState;
    private Dictionary<string, IPlayerState> states = new Dictionary<string, IPlayerState>();

    // ゲーム内で利用するステートを Dictionary に登録
    public void RegisterState(IPlayerState state)
    {
        if (!states.ContainsKey(state.stateType.ToString()))
        {
            states.Add(state.stateType.ToString(), state);
        }
    }

    // ステートの切り替え
    public void ChangeState(string stateType)
    {
        if (states.TryGetValue(stateType, out IPlayerState newState))
        {
            currentState?.Exit();

            // 新しいステートを登録し、開始処理を実行
            currentState = newState;
            currentState.Entry();
        }
    }

    public void Update()
    {
        // 現在のステートが設定されている場合
        if (currentState != null)
        {
            // 現在のステートの処理を実行
            currentState.Execute();
        }
    }
}
