using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyType { Static, Patrol }
    public EnemyType enemyType;

    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 4f;

    public float chaseRange = 10f;
    public float attackRange = 1.5f;

    public float attackCooldown = 1.2f;
    private float attackTimer;

    public int health = 3;
    public int damage = 1;

    public Transform player;
    public Transform pointA;
    public Transform pointB;

    private Transform currentTarget;
    private Rigidbody rb;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (enemyType == EnemyType.Patrol)
            currentTarget = pointA;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            if (enemyType == EnemyType.Patrol)
                Patrol();
            else
                StopMovement();
        }
    }

    void Patrol()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsRunning", false);

        MoveTowards(currentTarget.position, patrolSpeed);

        if (Vector3.Distance(transform.position, currentTarget.position) < 1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }

    void ChasePlayer(float distance)
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", true);

        if (distance > attackRange)
        {
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            StopMovement();
            Attack();
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0f;

        rb.linearVelocity = new Vector3(
            direction.x * speed,
            rb.linearVelocity.y,
            direction.z * speed
        );

        if (direction != Vector3.zero)
            transform.forward = direction;
    }

    void StopMovement()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    }

    void Attack()
    {
        if (attackTimer > 0f)
            return;

        attackTimer = attackCooldown;

        // Player can�n� burada d���r
        Debug.Log("Enemy attacked player!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Bullet(Clone)"))
        {
            health--;
            Destroy(other.gameObject);

            if (health <= 0)
                Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}