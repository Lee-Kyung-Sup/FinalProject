using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity,IDamageable
{
    LayerMask pLayer;
    [Header("MoveThings")]
    public float moveSpeed;
    public float idleTime;
    public float BattleTime;
    [Header("AttackThings")]
    public float attackDistance;
    public float attackDelay;
    public float dmg;
    public float hp;
    [HideInInspector]public float lastTimeAttacked;
    public EnemyStateMachine stateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        pLayer = LayerMask.GetMask("Player");
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState.Update();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, pLayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            StartCoroutine(Fx.HitFlash());
        }
    }
}
