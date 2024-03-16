using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Moving,
    Jumping,
    Dashing,
    Attacking
}

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;


    private void Update()
    {
        //TODO
        switch (currentState)
        {
            case PlayerState.Idle:
                
                break;
            case PlayerState.Moving:
                
                break;
            case PlayerState.Jumping:
                
                break;
            case PlayerState.Dashing:
                
                break;
            case PlayerState.Attacking:
                
                break;
        }
    }

}




