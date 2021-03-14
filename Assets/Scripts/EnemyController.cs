using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    public Transform enemyAttackPosition;
    public Animator animator;
    public ParticleSystem deathParticle;
    public LayerMask whatIsTarget;
    public float enemySpeed;
    public float stoppingDistance;
    public float maxHealth = 100f;
    public float attackRange;
    public float attackDamage = 10f;
    public float attackDelay;
    public float chasePlayerRange;

    private Transform _enemyTarget;
    private Transform _towerTarget;
    private GameObject _enemyCounter;
    private Rigidbody2D _rigidBody;
    private float _moveDirection;
    private bool _facingRight = true;
    private float _distToPlayer;
    private float _currentHealth;
    private float _nextAttackTime = 0f;
    private bool _isStun = false;
    private float _knockBackForceFromHit = 5f;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
        _enemyTarget = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _towerTarget = GameObject.FindWithTag("Tower").GetComponent<Transform>();
        _enemyCounter = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        _distToPlayer = Vector2.Distance(transform.position, _enemyTarget.position);
        PickTarget();
        AnimateFlip();
        animator.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x)); 
        CheckDie();
        AttackTarget();
    }

    void FixedUpdate() 
    {
        Move();
    }

    void PickTarget()
    {
        if (_distToPlayer < chasePlayerRange)
        {
            ChaseTarget(_enemyTarget);
        }
        else
        {
            ChaseTarget(_towerTarget);
        }
    }

    void ChaseTarget(Transform target)
    {
        if (transform.position.x < target.position.x)
        {
            _moveDirection = 1.0f * enemySpeed;    
        }
        else
        {
            _moveDirection = -1.0f * enemySpeed;
        }
    }

    void AttackTarget()
    {
        if (Time.time >= _nextAttackTime && _distToPlayer < stoppingDistance)
        {
            animator.SetTrigger("Attack");
            _nextAttackTime = Time.time + 1f / attackDelay;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(enemyAttackPosition.position, attackRange, whatIsTarget);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<PlayerController>().PlayerTakeDamage(attackDamage);
            }
        }
    }

    void Move()
    {
        if (!_isStun)
        {
            if (_distToPlayer > stoppingDistance)
            {
                _rigidBody.velocity = new Vector2(_moveDirection, _rigidBody.velocity.y);
            }
            else
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
        
    }

    IEnumerator StunTimer (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isStun = false;
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

    public void EnemyTakeDamage(float damage)
    {
        _currentHealth -= damage;
        animator.SetTrigger("GetHit");
        _isStun = true;
        if (_enemyTarget.transform.position.x > transform.position.x)
        {
            _rigidBody.velocity = new Vector2(-_knockBackForceFromHit, _rigidBody.velocity.y);
        }
        else
        {
            _rigidBody.velocity = new Vector2(_knockBackForceFromHit, _rigidBody.velocity.y);
        }
        StartCoroutine(StunTimer(2f));
    }

    void CheckDie()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        _enemyCounter.GetComponent<GameController>().EnemyCounter();
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyAttackPosition.position, attackRange);
    }
}
