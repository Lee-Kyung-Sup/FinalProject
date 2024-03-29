using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rigi;
    protected bool triggerCalled;
    string animBoolName;
    protected float stateTimer;
    public EnemyState(Enemy enemyBase,EnemyStateMachine stateMachine,string animBoolName)
    {
        this.animBoolName = animBoolName;
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        rigi = enemyBase.Rigi;
        enemyBase.Ani.SetBool(animBoolName,true);
    }
    public virtual void Exit()
    {
        enemyBase.Ani.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
