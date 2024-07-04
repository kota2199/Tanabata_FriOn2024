using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isJumping;
    private StateMachine stateMachine = new StateMachine();
    void Start()
    {
        playerAnimator = this.gameObject.GetComponent<Animator>();

        stateMachine.RegisterState(new PlayerStateStanding(stateMachine, this));
        stateMachine.RegisterState(new PlayerStateWalking(stateMachine, this));
        stateMachine.RegisterState(new PlayerStateJumping(stateMachine, this));      

        // 初期ステートの設定
        stateMachine.ChangeState(StateType.STANDING.ToString());
    }

    // Update is called once per frame
    void Update()
    {        
        // ステートを更新
        stateMachine.Update();
    }
}
