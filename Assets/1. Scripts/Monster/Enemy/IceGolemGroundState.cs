using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolemGroundState : EnemyState
{
    protected EnemyIceGolem iceGolem;
    public IceGolemGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,EnemyIceGolem iceGolem) : base(enemyBase, stateMachine, animBoolName)
    {
        this.iceGolem = iceGolem;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (iceGolem.IsPlayerDetected())
        {
            stateMachine.ChangeState(iceGolem.Battle);
        }
    }
}
