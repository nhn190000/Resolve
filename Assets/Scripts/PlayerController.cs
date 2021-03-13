using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundObject;
    public LayerMask whatIsEnemies;
    public Transform groundCheck;
    //public Transform enemy;
    public Animator animator;
    public Transform attackPosition;
    public Transform superAttackPosition;
    public float moveSpeed;
    public float jumpForce;
    public float groundCheckRadius = 0.2f;
    public float attackDelay;
    public float attackRange;
    public float superAttackRangeX;
    public float superAttackRangeY;
    public float attackDamage = 10f;
    public float superAttackDamage = 50f;
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public float initialResolvePoint = 0f;
    [HideInInspector] public float currentResolvePoint;

    private Rigidbody2D _rigidBody;
    private float _moveDirection;
    private bool _facingRight = true;
    private bool _isJumping = false;
    private bool _isGrounded;
    private float _nextAttackTime = 0f;
    private int _normalAttack = 1;
    private int _superAttack = 2;
    private bool _isDead = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentResolvePoint = initialResolvePoint;
    }

    void Update()
    {
        ProcessInput();
        AnimateFlip();
        CheckDie();
    }

    void FixedUpdate() 
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundObject);
        Move();
        animator.SetBool("Jumping", !_isGrounded);
    }

    void ProcessInput()
    {
        _moveDirection = Input.GetAxis("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(_moveDirection));

        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
            animator.SetBool("Jumping", _isJumping);
        }

        if (Input.GetButtonDown("Fire1") && _isGrounded && Time.time >= _nextAttackTime)
        {
            Attack(_normalAttack);
            _nextAttackTime = Time.time + 1f / attackDelay;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().EnemyTakeDamage(attackDamage);
                Resolve();
            }
        }

        if (Input.GetButtonDown("Fire2") && _isGrounded && Time.time >= _nextAttackTime && currentResolvePoint >= 0.5f)
        {
            Attack(_superAttack);
            _nextAttackTime = Time.time + 1f / attackDelay;
            currentResolvePoint -= 0.5f;
            Collider2D[] enemiesToSuperDamage = Physics2D.OverlapBoxAll(superAttackPosition.position, new Vector2(superAttackRangeX, superAttackRangeY), 0, whatIsEnemies);
            for (int i = 0; i < enemiesToSuperDamage.Length; i++)
            {
                enemiesToSuperDamage[i].GetComponent<EnemyController>().EnemyTakeDamage(superAttackDamage);
            }
        }
    }

    void AnimateFlip()
    {
        if (_moveDirection > 0 && !_facingRight)
        {
            FlipCharacter();
        }

        if (_moveDirection < 0 && _facingRight)
        {
            FlipCharacter();
        }
    }

    void FlipCharacter()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
        
    void Move()
    {
        _rigidBody.velocity = new Vector2(_moveDirection, _rigidBody.velocity.y);

        if (_isJumping && _isGrounded)
        {
            _rigidBody.AddForce(new Vector2(0f, jumpForce));
        }

        _isJumping = false;
    }

    void Attack(int attackType)
    {
        if (attackType == 1)
        {
            animator.SetTrigger("Attacking");
        }
        else if (attackType == 2)
        {
            animator.SetTrigger("SuperAttacking");
        }
    }

    void Resolve()
    {
        if (currentResolvePoint < 1f)
        {
            currentResolvePoint += 0.1f;
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        if (_isDead == false)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");
        }
    }

    void CheckDie()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        animator.SetBool("isDead", true);
        _isDead = true;
        this.enabled = false;
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
        Gizmos.DrawWireCube(superAttackPosition.position, new Vector3(superAttackRangeX, superAttackRangeY, 1));
    }
}
