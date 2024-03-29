using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public EnemyMeleeIdle Idle { get; private set; }
    public EnemyMeleeMove Move { get; private set; }
    public EnemyMeleeBattle Battle { get; private set; }
    public EnemyMeleeAttack Attack { get; private set; }
    public EnemyMeleeDie Diea { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new EnemyMeleeIdle(this,stateMachine,"Idle",this);
        Move = new EnemyMeleeMove(this,stateMachine,"Move",this);
        Battle = new EnemyMeleeBattle(this, stateMachine, "Move", this);
        Attack = new EnemyMeleeAttack(this, stateMachine, "Attack", this);
        Diea = new EnemyMeleeDie(this, stateMachine, "Die", this);
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
public class EnemyMeleeGroundState : EnemyState
{
    protected EnemyMelee EnemyMelee;
    protected Transform player;
    public EnemyMeleeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(enemyBase, stateMachine, animBoolName)
    {
        this.EnemyMelee = EnemyMelee;
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
        if (EnemyMelee.hp < 1)
        {
            stateMachine.ChangeState(EnemyMelee.Diea);
            return;
        }
        if (EnemyMelee.IsPlayerDetected() || Vector2.Distance(EnemyMelee.transform.position, player.position) < 3)
        {
            stateMachine.ChangeState(EnemyMelee.Battle);
        }
    }
}

public class EnemyMeleeIdle : EnemyMeleeGroundState
{
    public EnemyMeleeIdle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(enemyBase, stateMachine, animBoolName, EnemyMelee)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = EnemyMelee.idleTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (EnemyMelee.hp < 1)
        {
            stateMachine.ChangeState(EnemyMelee.Diea);
            return;
        }
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(EnemyMelee.Move);
        }
    }
}
public class EnemyMeleeMove : EnemyMeleeGroundState
{
    public EnemyMeleeMove(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(enemyBase, stateMachine, animBoolName, EnemyMelee)
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
        if (EnemyMelee.hp < 1)
        {
            stateMachine.ChangeState(EnemyMelee.Diea);
            return;
        }
        EnemyMelee.SetVelocity(EnemyMelee.moveSpeed * EnemyMelee.facingDir, EnemyMelee.Rigi.velocity.y);
        if (EnemyMelee.IsWallDetected() || !EnemyMelee.IsGroundDetected())
        {
            Debug.Log(EnemyMelee.IsWallDetected());
            Debug.Log(!EnemyMelee.IsGroundDetected());
            EnemyMelee.Flip();
            stateMachine.ChangeState(EnemyMelee.Idle);
        }
    }
}
public class EnemyMeleeBattle : EnemyState
{
    EnemyMelee EnemyMelee;
    Transform player;
    int moveDir;
    public EnemyMeleeBattle(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(EnemyMelee, stateMachine, animBoolName)
    {
        this.EnemyMelee = EnemyMelee;
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
        if (EnemyMelee.hp < 1)
        {
            stateMachine.ChangeState(EnemyMelee.Diea);
            return;
        }
        if (EnemyMelee.IsPlayerDetected())
        {
            stateTimer = EnemyMelee.BattleTime;
            if (EnemyMelee.IsPlayerDetected().distance < EnemyMelee.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(EnemyMelee.Attack);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.position,EnemyMelee.transform.position) > 15)
            {
                stateMachine.ChangeState(EnemyMelee.Idle);
            }
        }
        if (player.position.x > EnemyMelee.transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }
        EnemyMelee.SetVelocity(EnemyMelee.moveSpeed * moveDir, EnemyMelee.Rigi.velocity.y);
    }
    bool CanAttack()
    {
        if (Time.time >= EnemyMelee.lastTimeAttacked + EnemyMelee.attackDelay)
        {
            EnemyMelee.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
public class EnemyMeleeAttack : EnemyState
{
    EnemyMelee EnemyMelee;
    public EnemyMeleeAttack(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(EnemyMelee, stateMachine, animBoolName)
    {
        this.EnemyMelee = EnemyMelee;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        EnemyMelee.lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();
        if (EnemyMelee.hp < 1)
        {
            stateMachine.ChangeState(EnemyMelee.Diea);
            return;
        }
        EnemyMelee.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(EnemyMelee.Battle);
        }
    }
}
public class EnemyMeleeDie : EnemyState
{
    EnemyMelee EnemyMelee;
    public EnemyMeleeDie(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemyMelee EnemyMelee) : base(EnemyMelee, stateMachine, animBoolName)
    {
        this.EnemyMelee = EnemyMelee;
    }
    public override void Enter()
    {
        base.Enter();
        EnemyMelee.Die();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (EnemyMelee.Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            EnemyMelee.Destroy();
        }
    }
}