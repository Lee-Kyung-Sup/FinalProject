using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceGolem : Enemy
{
    public IceGolemIdle Idle { get; private set; }
    public IceGolemMove Move { get; private set; }
    public IceGolemBattle Battle { get; private set; }
    public IceGolemAttack Attack { get; private set; }
    public IceGolemDie Die { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new IceGolemIdle(this,stateMachine,"Idle",this);
        Move = new IceGolemMove(this,stateMachine,"Move",this);
        Battle = new IceGolemBattle(this, stateMachine, "Move", this);
        Attack = new IceGolemAttack(this, stateMachine, "Attack", this);
        Die = new IceGolemDie(this, stateMachine, "Die", this);
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
public class IceGolemGroundState : EnemyState
{
    protected EnemyIceGolem iceGolem;
    protected Transform player;
    public IceGolemGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem iceGolem) : base(enemyBase, stateMachine, animBoolName)
    {
        this.iceGolem = iceGolem;
    }
    public override void Enter()
    {
        base.Enter();
        player = GameManager.instance.player.transform;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (iceGolem.hp < 1)
        {
            stateMachine.ChangeState(iceGolem.Die);
            return;
        }
        if (iceGolem.IsPlayerDetected() || Vector2.Distance(iceGolem.transform.position, player.position) < 3)
        {
            stateMachine.ChangeState(iceGolem.Battle);
        }
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
        if (iceGolem.hp < 1)
        {
            stateMachine.ChangeState(iceGolem.Die);
            return;
        }
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
        if (iceGolem.hp < 1)
        {
            stateMachine.ChangeState(iceGolem.Die);
            return;
        }
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
    EnemyIceGolem iceGolem;
    Transform player;
    int moveDir;
    public IceGolemBattle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem iceGolem) : base(iceGolem, stateMachine, animBoolName)
    {
        this.iceGolem = iceGolem;
    }
    public override void Enter()
    {
        base.Enter();
        player = GameManager.instance.player.transform;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (iceGolem.hp < 1)
        {
            stateMachine.ChangeState(iceGolem.Die);
            return;
        }
        if (iceGolem.IsPlayerDetected())
        {
            stateTimer = iceGolem.BattleTime;
            if (iceGolem.IsPlayerDetected().distance < iceGolem.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(iceGolem.Attack);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position,iceGolem.transform.position) > 15)
            {
                stateMachine.ChangeState(iceGolem.Idle);
            }
        }
        if (player.position.x > iceGolem.transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }
        iceGolem.SetVelocity(iceGolem.moveSpeed * moveDir, iceGolem.Rigi.velocity.y);
    }
    bool CanAttack()
    {
        if (Time.time >= iceGolem.lastTimeAttacked + iceGolem.attackDelay)
        {
            iceGolem.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
public class IceGolemAttack : EnemyState
{
    EnemyIceGolem iceGolem;
    public IceGolemAttack(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem IceGolem) : base(IceGolem, stateMachine, animBoolName)
    {
        this.iceGolem = IceGolem;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        iceGolem.lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();
        if (iceGolem.hp < 1)
        {
            stateMachine.ChangeState(iceGolem.Die);
            return;
        }
        iceGolem.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(iceGolem.Battle);
        }
    }
}
public class IceGolemDie : EnemyState
{
    EnemyIceGolem iceGolem;
    public IceGolemDie(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyIceGolem IceGolem) : base(IceGolem, stateMachine, animBoolName)
    {
        this.iceGolem = IceGolem;
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
    }
}