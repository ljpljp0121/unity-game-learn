using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    //进入游戏的第一个状态
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    //改变状态
    public void ChangeState(EnemyState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
