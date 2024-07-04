using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunning : IPlayerState
{
    private StateMachine stateMachine;
    public StateType stateType => StateType.RUNNING;
    private Player player;
    public PlayerStateRunning(StateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }
    public void Entry()
    {
        Debug.Log("Running");

        player.playerAnimator.SetBool("isRunning", true);
    }
    public void Execute()
    {
        
    }
    public void Exit()
    {
        player.playerAnimator.SetBool("isRunning", false);
     }
}
