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
    public float attackRange = 1f;
    protected CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    public enum State
    {
        Idle,
        Move
        
        
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
        if (playerinRange == true)
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
        MoveTo();
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        currentState = State.Move;
        Debug.Log("C");

    }

    public void Animmoveto()
    {
        StopAllCoroutines();
        StartCoroutine(Animmove());
    }

    IEnumerator Animmove()
    {
        yield return Delay500;
        rb.velocity = new Vector2(0, 0);
        MoveTo();
        yield break;

    }
    public void MoveTo()
    {
        Debug.Log(rb.velocity);
       
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position - new Vector3(0, 0.22f, 0), moveSpeed * Time.deltaTime);
        Debug.Log("Hitend");
    }
    //맞고나서 박쥐속도 조절


    //IEnumerator Attack()
    //{
    //    yield return null;

    //    if (canAttack == true)
    //    {
    //        canAttack = false;
    //        canAtk = false;
    //        MyAnimSetTrigger(currentState.ToString());
    //        rb.velocity = new Vector2(transform.localScale.x*2f, transform.localScale.y);

    //        yield return null;
    //        currentState = State.Attack;


    //    }
    //    else
    //    {
    //        yield return null;
    //        currentState = State.Move;
    //    }

    //}

   
    void Update()
    {
        
        MyAnimSetTrigger(currentState.ToString());
        float distancetoPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distancetoPlayer < detectionRange)
        {
            playerinRange = true;
            //MoveTo();
            currentState = State.Move;
            //if(distancetoPlayer < attackRange)
            //{
            //    canAttack = true;

            //}
            //else
            //{
            //    canAttack = false;
            //}
            Debug.Log("A");
        }
        else
        {
            playerinRange = false;
            currentState = State.Idle;
            Debug.Log("B");
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
