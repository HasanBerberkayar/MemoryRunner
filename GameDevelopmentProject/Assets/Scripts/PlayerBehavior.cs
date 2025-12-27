using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private float vInput;
    private float hInput;
    private Rigidbody rb;

    public float jumpVelocity = 5f;
    private bool isJumping;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    private CapsuleCollider col;
    public int maxJump = 2;
    public int jumpCount;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool isShooting;

    public GameObject cam;
    public float shoulderOffset = 1f;
    public float bulletHeight = 0.5f;

    public float fireRate = 0.3f;
    private float fireTimer = 0f;

    public int bulletMax = 10;
    public int bulletRemaining;
    public float reloadTime = 2f;
    public float reloadTimer = 0f;
    private bool isReloading = false;

    private Animator anim;
    public TMP_Text bulletText;

    private int hidePressAmount = 0;
    public bool canEnemysSee = true;

    private int maxHealth = 5;
    private int currentHealth;
    public TMP_Text healthText;

    // Unity Message | 0 references
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        bulletRemaining = bulletMax;
        jumpCount = maxJump;
        currentHealth = maxHealth;
    }

    // Unity Message | 0 references
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Hide();
            hidePressAmount++;
        }

        vInput = Input.GetAxis("Vertical") * MoveSpeed;
        hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        isJumping |= Input.GetKeyDown(KeyCode.Space);
        isShooting |= Input.GetMouseButton(0);

        bool isWalking = Mathf.Abs(vInput) > 0.01f;
        if (!canEnemysSee)
        {
            isWalking = false;
        }
        anim.SetBool("IsWalking", isWalking);

        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
        }
        if ((bulletRemaining <= 0 || isReloading) && reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            isReloading = true;
        }
        else if (reloadTimer <= 0)
        {
            bulletRemaining = bulletMax;
            reloadTimer = reloadTime;
            isReloading = false;
            UpdateBulletText();
        }

        if (isReloading)
        {
            MoveSpeed = 5;
        }
        else
        {
            MoveSpeed = 10;
        }
    }

    // Unity Message | 0 references
    void FixedUpdate()
    {
        Move();
        Jump();
        Shoot();
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    private void Move()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRot);
    }


    private void Jump()
    {
        if (IsGrounded())
        {
            jumpCount = maxJump;
        }
        if (jumpCount > 1 && isJumping)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpCount--;
        }
        isJumping = false;
    }


    private void Shoot()
    {
        if (isShooting && fireTimer <= 0f && bulletRemaining > 0)
        {
            Vector3 spawnPos;

            if (cam.GetComponent<CameraBehavior>().isLeftShoulder)
            {
                spawnPos = transform.position - transform.right * shoulderOffset + Vector3.up * bulletHeight;
            }
            else
            {
                spawnPos = transform.position + transform.right * shoulderOffset + Vector3.up * bulletHeight;
            }
            GameObject newBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.linearVelocity = (cam.transform.forward + Vector3.up * 0.05f) * bulletSpeed;
            fireTimer = fireRate;
            bulletRemaining = bulletRemaining - 1;
            UpdateBulletText();
        }
        isShooting = false;
    }

    private void UpdateBulletText()
    {
        bulletText.text = "Bullet: " + bulletRemaining.ToString();
    }

    private void Hide()
    {
        if (hidePressAmount%2 == 0)
        {
            canEnemysSee = false;
            MoveSpeed = MoveSpeed / 2;
            anim.SetBool("IsHiding", true);
        }
        else
        {
            canEnemysSee = true;
            MoveSpeed = MoveSpeed * 2;
            anim.SetBool("IsHiding", false);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        healthText.text = "Health: " + currentHealth.ToString();

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Die()
    {

    }
}