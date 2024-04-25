using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;



public class PlayerStatus : MonoBehaviour, IDamageable
{
    public Dictionary<AttackTypes, int> attackPower;

    public class Stat
    {
        public int health { get; set; }

        public Stat(int health)
        {
            this.health = health; 
        }
    }
    void StatInit()
    {
        playerHealth = new Stat(3);

        attackPower = new Dictionary<AttackTypes, int>
        {
            { AttackTypes.MeleeAttack, 3 },
            { AttackTypes.RangeAttack, 1 },
            { AttackTypes.JumpAttack, 3 },
            { AttackTypes.ComboAttack1, 3 },
            { AttackTypes.ComboAttack2, 5 },
            { AttackTypes.ChargeShot, 3 },
            { AttackTypes. DeflectionAttack, 5 },
        };
    }


    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerUI playerUI;
    private SaveNLoad theSaveNLoad;//저장을 위한 추가작성 JHP
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Stats Parameter")]
    public Stat playerHealth;
    [SerializeField] private float stamina = 100;
    [SerializeField] private float staminaRecoveryRate = 50;
    [SerializeField] private float staminaRecoveryDelay = 0.5f;

    private float lastStaminaUseTime;
    private float maxStamina;

    [Header("Movement Parameter")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2;

    private bool isDamaged = false;


    public float Speed => speed;
    public float JumpPower => jumpPower;
    public int MaxJumpCount => maxJumpCount;
    public float Stamina => stamina;

    //public string currentSceneName = "2. GameScene"; //저장을 위한 추가작성 JHP

    void Start()
    {
        StatInit();

        playerUI = FindObjectOfType<PlayerUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        theSaveNLoad = FindObjectOfType<SaveNLoad>(); //저장을 위한 추가작성 JHP
        lastStaminaUseTime = Time.time;
        maxStamina = stamina;
    }


    void Update()
    {
        if (Time.time - lastStaminaUseTime >= staminaRecoveryDelay && stamina < 100)
        {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }

        float Stamina = Mathf.Lerp(playerUI.staminaUI.value, maxStamina, Time.deltaTime * 10);
        playerUI.UpdateStaminaUI(Stamina);

        if(Input.GetKeyDown(KeyCode.F5))//저장을 위한 추가작성 JHP
        {
            theSaveNLoad.CallSave();
        }
        if (Input.GetKeyDown(KeyCode.F9))//저장을 위한 추가작성 JHP
        {
            theSaveNLoad.CallLoad();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDamaged) return;

        AudioManager.Instance.PlaySFX("OnDamaged");
        OnInvincible();
        isDamaged = true;
        playerAnimations.GetHit();
        playerHealth.health -= damage;
        playerUI.UpdateHeartUI(playerHealth.health);

        if (playerHealth.health <= 0)
        {
            AudioManager.Instance.PlaySFX("Death");
            PlayerDead();
        }
        StartCoroutine(ResetDamage()); // 일정 시간 후에 isDamaged 리셋
    }
    private IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(3.0f); 
        isDamaged = false;
    }

    public void OnInvincible()
    {
        gameObject.layer = 18;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        StartCoroutine(BlinkEffect(3f, 0.1f)); 
    }

    private IEnumerator BlinkEffect(float duration, float interval)
    {
        float time = 0;
        while (time < duration)
        {
            if (spriteRenderer.color.a == 0.5f)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.7f);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }

            yield return new WaitForSeconds(interval);
            time += interval;
        }
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void UseStamina(float value)
    {
        stamina -= value;

        if (stamina < 0)
        {
            stamina = 0;
        }
        maxStamina = stamina;
        lastStaminaUseTime = Time.time;
        playerUI.UpdateStaminaUI(stamina);
    }

    private void RecoverStamina(float value)
    {
        stamina += value;
        if (stamina > 100)
        {
            stamina = 100;
        }
        maxStamina = stamina;
    }

    private void PlayerDead() // 플레이어 죽음
    {
        playerAnimations.Dead();
        GameManager.Instance.OnGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & ((1 << 7)  |(1 << 12))) != 0) // 7. 몬스터 ,  12. 에네미 불렛
        { 
            TakeDamage(1);
            playerMovement.OnKnockback(collision.transform.position);
        }
    }
}




