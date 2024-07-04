using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateStanding : IPlayerState
{
    private StateMachine stateMachine = new StateMachine();
    public StateType stateType => StateType.STANDING;
    private Player player;
    public PlayerStateStanding(StateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    public void Entry()
    { 
        Debug.Log("Standing");
    }
    public void Execute() 
    {
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            stateMachine.ChangeState(StateType.WALKING.ToString());
        }

        if(Input.GetKeyDown(KeyCode.Space) && !player.isJumping)
        {
            stateMachine.ChangeState(StateType.JUMPING.ToString());
        }
    }
    public void Exit() { /*...*/ }
}
