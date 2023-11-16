using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine

{
    public PlayerState currentState { get; private set; }
    //进入游戏的第一个状态
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    //改变状态
    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}