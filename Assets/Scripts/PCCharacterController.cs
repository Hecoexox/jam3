using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform lantern;
    public Transform canbari;
    public Vector2 lanternOffset = new Vector2(0.5f, 0.0f);
    public Vector2 canbariOffset = new Vector2(0f, 0f);

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canInteract = false;
    private GameObject tosbagaObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (!gameObject.activeSelf) return; // Eðer karakter aktif deðilse hareket etme

        float moveDirection = Input.GetAxisRaw("Horizontal");
        bool isJumped = Input.GetKeyDown(KeyCode.W);
        bool isSpeaking = Input.GetKey(KeyCode.LeftShift);
        bool isAttacking = Input.GetKey(KeyCode.E);

        // Eðer karakter fýrlatýlýyorsa, hareketi güncelleme!
        if (Mathf.Abs(rb.velocity.x) > moveSpeed || Mathf.Abs(rb.velocity.y) > jumpForce)
        {
            return; // Fýrlatma yapýldýysa bu karede hareket kodlarýný çalýþtýrma
        }

        // Hareketi Rigidbody2D ile saðla
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Zýplama (Sadece yerdeyken)
        if (isJumped && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }

        // Saldýrý giriþini kontrol et
        if (isAttacking)
        {
            animator.SetTrigger("Attack");
        }

        // Tosbaga ile etkileþim kontrolü
        if (canInteract && Input.GetKeyDown(KeyCode.E) && tosbagaObject != null)
        {
            tosbagaObject.SetActive(false);
        }

        animator.SetBool("isWalking", moveDirection != 0 && !isSpeaking && !isAttacking);
        animator.SetBool("isSpeaking", isSpeaking);
        animator.SetBool("isAttacking", isAttacking);

        if (moveDirection != 0)
        {
            bool facingLeft = moveDirection < 0;
            spriteRenderer.flipX = facingLeft;

            // Fener ve canbarý yönünü ayarla
            if (lantern != null)
            {
                Vector3 scale = lantern.localScale;
                scale.x = facingLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                lantern.localScale = scale;
                lantern.localPosition = new Vector3(facingLeft ? -lanternOffset.x : lanternOffset.x, lanternOffset.y, 0);
            }

            if (canbari != null)
            {
                Vector3 scale = canbari.localScale;
                scale.x = facingLeft ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                canbari.localScale = scale;
                canbari.localPosition = new Vector3(facingLeft ? -canbariOffset.x : canbariOffset.x, canbariOffset.y, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tosbaga"))
        {
            canInteract = true;
            tosbagaObject = collision.gameObject;
        }
    }
}
