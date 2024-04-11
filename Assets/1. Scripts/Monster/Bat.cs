using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bat : Monster
{

    public GameObject player;
    
    private bool playerinRange = false;
    public float detectionRange = 20f;
    private bool canAttack = false;
    public float attackRange = 3f;
    protected CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    public enum State
    {
        Idle,
        Move,
        Attack
        
    };

    public State currentState = State.Idle;
    WaitForSeconds Delay500 = new WaitForSeconds(0.55f);

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 6f;
        currentHp = 100;
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
            currentState = State.Move;
        }
        else
        {
            currentState = State.Idle;
        }
    }
    
    IEnumerator Move()
    {
        yield return null;
        MyAnimSetTrigger(currentState.ToString());
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if(canAttack == true)
        {
            currentState = State.Attack;
            
        }
        else
        {
            currentState = State.Move;
        }
    }



    IEnumerator Attack()
    {
        yield return null;

        if (canAttack == true)
        {
            canAttack = false;
            canAtk = false;
            MyAnimSetTrigger(currentState.ToString());
            rb.velocity = new Vector2(player.transform.localScale.x*2f, player.transform.localScale.y);
            
            yield return Delay500;
            currentState = State.Move;

            
        }
        else
        {
            yield return null;
            currentState = State.Move;
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
