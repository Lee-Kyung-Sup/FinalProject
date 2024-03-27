using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    protected bool triggerCalled;
    string animBoolName;
    protected float stateTimer;
    public EnemyState(Enemy enemy,EnemyStateMachine stateMachine,string animBoolName)
    {
        this.animBoolName = animBoolName;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        enemy.Ani.SetBool(animBoolName,true);
    }
    public virtual void Exit()
    {
        enemy.Ani.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
}
