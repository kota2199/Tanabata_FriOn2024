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
<<<<<<< Updated upstream
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)
         || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
=======
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
>>>>>>> Stashed changes
        {
            stateMachine.ChangeState(StateType.STANDING.ToString());
        }
    }
    public void Exit()
    {
        player.playerAnimator.SetBool("isWalking", false);
    }
}
