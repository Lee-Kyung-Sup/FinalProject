using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceGolem : Enemy
{
    public IceGolemIdle Idle { get; private set; }
    public IceGolemMove Move { get; private set; }
    public IceGolemBattle Battle { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new IceGolemIdle(this,stateMachine,"Idle",this);
        Move = new IceGolemMove(this,stateMachine,"Move",this);
        Battle = new IceGolemBattle(this, stateMachine, "Move", this);
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
public class IceGolemIdle : IceGolemGroundState
{
    public IceGolemIdle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem iceGolem) : base(enemyBase, stateMachine, animBoolName, iceGolem)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = iceGolem.idleTime;
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
            stateMachine.ChangeState(iceGolem.Move);
        }
    }
}
public class IceGolemMove : IceGolemGroundState
{
    public IceGolemMove(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem iceGolem) : base(enemyBase, stateMachine, animBoolName, iceGolem)
    {
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
        iceGolem.SetVelocity(iceGolem.moveSpeed * iceGolem.facingDir, iceGolem.Rigi.velocity.y);
        if (iceGolem.IsWallDetected() || iceGolem.IsGroundDetected())
        {
            iceGolem.Flip();
            stateMachine.ChangeState(iceGolem.Idle);
        }
    }
}
public class IceGolemBattle : EnemyState
{
    EnemyIceGolem IceGolem;
    Transform player;
    int moveDir;
    public IceGolemBattle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem IceGolem) : base(IceGolem, stateMachine, animBoolName)
    {
        this.IceGolem = IceGolem;
    }
    public override void Enter()
    {
        base.Enter();
        player = IceGolem.IsPlayerDetected().transform;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (enemyBase.IsPlayerDetected())
        {
            if (IceGolem.IsPlayerDetected().distance < enemyBase.attackDistance)
            {
                enemyBase.ZeroVelocity();
                return;
            }
        }
        if (player.position.x > enemyBase.transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }
        enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, enemyBase.Rigi.velocity.y);
    }
}