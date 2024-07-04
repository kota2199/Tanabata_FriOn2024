using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunning : IPlayerState
{
    private StateMachine stateMachine;
    public StateType stateType => StateType.RUNNING;

    public void Entry() { /*...*/ }
    public void Execute() { /*...*/ }
    public void Exit() { /*...*/ }
}
