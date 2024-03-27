using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceGolem : Enemy
{
    public IceGolemIdle Idle { get; private set; }
    public IceGolemMove Move { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new IceGolemIdle(this,stateMachine,"Idle",this);
        Move = new IceGolemMove(this,stateMachine,"Move",this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(Idle);
    }
    protected override void Update()
    {
        base.Update();
    }
}
public class IceGolemIdle : EnemyState
{
    EnemyIceGolem IceGolem;
    public IceGolemIdle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,EnemyIceGolem IceGolem) : base(IceGolem, stateMachine, animBoolName)
    {
        this.IceGolem = IceGolem;
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = IceGolem.idleTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(IceGolem.Move);
        }
    }
}
public class IceGolemMove : EnemyState
{
    EnemyIceGolem IceGolem;
    public IceGolemMove(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem IceGolem) : base(IceGolem, stateMachine, animBoolName)
    {
        this.IceGolem = IceGolem;
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
        IceGolem.SetVelocity(IceGolem.moveSpeed * IceGolem.facingDir, IceGolem.Rigi.velocity.y);
        if (IceGolem.IsWallDetected() || IceGolem.IsGroundDetected())
        {
            IceGolem.Flip();
            stateMachine.ChangeState(IceGolem.Idle);
        }
    }
}