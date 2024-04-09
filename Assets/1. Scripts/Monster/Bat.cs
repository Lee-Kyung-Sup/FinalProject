using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bat : Monster
{

    public GameObject player;
    
    private bool playerinRange = false;
    public float detectionRange = 20f;
    private bool canAttack = false;
    public float attackRange = 1f;
    protected CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    public enum State
    {
        Idle,
        Attack
        
    };

    public State currentState = State.Idle;
    WaitForSeconds Delay500 = new WaitForSeconds(0.55f);

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 5f;
        circleCollider = GetComponent<CircleCollider2D>();
        atkCoolTime = 2f;
        atkCoolTimeCalc = atkCoolTime;
        StartCoroutine(FSM());
    }

    public void Start()
    {
        player = GameManager.Instance.Player;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    IEnumerator Idle()
    {
        
        MyAnimSetTrigger(currentState.ToString());
        yield return Delay500;
        if(playerinRange == true)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Idle;
        }
    }
    
    IEnumerator Attack()
    {
        yield return null;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if (canAttack == true)
        {
            canAttack = false;
            canAtk = false;
            MyAnimSetTrigger(currentState.ToString());
            rb.velocity = new Vector2(player.transform.localScale.x, player.transform.localScale.y * 1f);
            yield return Delay500;
            currentState = State.Attack;
        }
    }
    
    
    void Update()
    {
        float distancetoPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distancetoPlayer < detectionRange)
        {
            playerinRange = true;
            if(distancetoPlayer < attackRange)
            {
                canAttack = true;
                
            }
            else
            {
                canAttack = false;
            }
        }
        else
        {
            playerinRange = false;
        }

        if(player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
