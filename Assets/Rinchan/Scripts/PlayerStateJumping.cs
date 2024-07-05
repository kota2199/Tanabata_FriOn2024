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
    }
    public void Execute()
    {
        player.playerAnimator.SetTrigger("Jumping");
    }
    public void Exit()
    {
        //player.playerAnimator.ResetTrigger("Jumping");
    }
}
