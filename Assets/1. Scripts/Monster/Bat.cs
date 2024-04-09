using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{

    public GameObject player;
    protected Rigidbody2D rb;
    private bool playerinRange = false;
    public float detectionRange = 7f;
    private bool canAttack = false;
    public float attackRange = 3f;
    protected CircleCollider2D circleCollider;
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
        moveSpeed = 4f;
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FSM());
    }

    public void Start()
    {
        player = GameManager.Instance.Player;
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
        
    }
    
    IEnumerator Attack()
    {
        yield return null;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        if (canAtk == true)
        {
            canAtk = false;
            MyAnimSetTrigger(currentState.ToString());
            rb.velocity = new Vector2(-transform.localScale.x * 14f, 1f);
            yield return null;
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
                canAtk = true;
                
            }
            else
            {
                canAtk = false;
            }
        }
        else
        {
            playerinRange = false;
        }

        Vector3 direction = player.position = transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
