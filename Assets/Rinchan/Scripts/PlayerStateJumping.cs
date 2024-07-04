using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJumping : IPlayerState
{
    private StateMachine stateMachine;
    public StateType stateType => StateType.JUMPING;
    private Player player;
    public PlayerStateJumping(StateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    public void Entry()
    {
        Debug.Log("Jumping");
        player.isJumping = true;
    }
    public void Execute()
    {
        player.playerAnimator.SetTrigger("Jumping");

        if(player.isJumping && !player.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jumping@loop"))
        {
            player.isJumping = false;
            player.playerAnimator.ResetTrigger("Jumping");
        }
    }
    public void Exit()
    {
        //player.playerAnimator.ResetTrigger("Jumping");
    }
}
