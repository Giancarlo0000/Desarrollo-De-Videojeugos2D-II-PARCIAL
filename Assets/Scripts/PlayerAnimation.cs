using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private bool isGrounded = false;

    public Player Player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(Player.groundCheck.position, Player.groundCheckRadius, Player.groundLayer);

        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        float axis = Input.GetAxis("Horizontal");

        bool isRunning = axis != 0 ? true : false;

        if (isRunning)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        
        anim.SetFloat("Falling", rb.velocity.y);
        anim.SetBool("isOnGround", isGrounded);

        if (isGrounded && !isRunning)
        {
            anim.Play("Idle");
        }

        if (isGrounded && isRunning)
        {
            anim.Play("Run");
        }
    }
}
