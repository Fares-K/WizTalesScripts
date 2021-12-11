using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]private LayerMask jumpGround;

    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    int jumpCount = 0;
    int extraJumps = 1;
    [SerializeField] Transform feet;
    bool isgrounded;
    float jumpCoolDown;

    public float dashDistance = 15f;
    bool isDashing;
    float doubletaptime;
    KeyCode lastkeyCode;

    private enum moveState { idle, walking, jumping }

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if(!isDashing)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        
        

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        checkGrounded();

        UpdateAnimationState();

        //Dash Left
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(doubletaptime > Time.time && lastkeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
                anim.SetTrigger("Dash");
                
            }
            else
            {
                doubletaptime = Time.time + 0.5f;
            }

            lastkeyCode = KeyCode.A;
        }

        //Dash Right


        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubletaptime > Time.time && lastkeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1f));
                anim.SetTrigger("Dash");
            }
            else
            {
                doubletaptime = Time.time + 0.5f;
            }

            lastkeyCode = KeyCode.D;
        }

    }

    private void UpdateAnimationState()
    {

        moveState state;
        
        if (dirX > 0f)
        {
            state = moveState.walking;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = moveState.walking;
            sprite.flipX = true;
        }
        else
        {
            state = moveState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = moveState.jumping;
        }

        else if(rb.velocity.y < -0.1f)
        {
            state = moveState.jumping;
        }

        anim.SetInteger("state", (int)state);
    }


    IEnumerator Dash (float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;
    }

    void Jump()
    {
        if(isgrounded || jumpCount < extraJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
        
    }

    void checkGrounded()
    {
        if (Physics2D.OverlapCircle(feet.position, 0.5f, jumpGround))
        {
            isgrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;

        }
        else if(Time.time < jumpCoolDown)
        {
            isgrounded = true;
        }
        else
        {
            isgrounded = false;
        }
    }
}
