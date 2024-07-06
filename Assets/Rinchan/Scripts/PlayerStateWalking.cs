using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalking : IPlayerState
{
    private StateMachine stateMachine = new StateMachine();
    public StateType stateType => StateType.WALKING;
    private Player player;
    public PlayerStateWalking(StateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    public void Entry()
    {
        Debug.Log("Walking");
        player.playerAnimator.SetBool("isWalking", true);
    }
    public void Execute()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            stateMachine.ChangeState(StateType.STANDING.ToString());
        }
    }
    public void Exit()
    {
        player.playerAnimator.SetBool("isWalking", false);
    }
}
