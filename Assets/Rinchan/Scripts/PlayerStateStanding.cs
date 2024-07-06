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
        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) {
        //if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
        // || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))

        //{
            stateMachine.ChangeState(StateType.WALKING.ToString());
        }
    }
    public void Exit()
    {

    }
}
