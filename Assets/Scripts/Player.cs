using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Descripción: Personaje que es controlado por el jugador, tiene que llegar a una meta antes de que se agote el tiempo mientras evita perder todas sus vidas por los enemigos.
    public float speed;
    public float maxJumpForce;
    public float minJumpForce;
    public float jumpTime;
    public float jumpForce;

    [SerializeField]private float lives;
    public float Lives //Propiedades
    {
        get { return lives; }
        set { lives = value; }
    }

    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public GameObject redWall;
    public GameObject yellowWall;
    public GameObject greenWall;
    public GameObject goal;

    private bool isGrounded = false;
    private bool isLookingRight = true;
    private bool isJumping = false;
    private float jumpTimer;
    private Rigidbody2D rb;

    public bool isLevelCompleted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movimiento
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float dirX = Input.GetAxis("Horizontal");
        if (dirX < 0 && isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isLookingRight = false;
        }
        if (dirX > 0 && !isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isLookingRight = true;
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > jumpTime)
            {
                jumpTimer = jumpTime;
                isJumping = false;
            }
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minJumpForce, maxJumpForce, jumpTimer / jumpTime));
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        print(jumpTimer);
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        if (isGrounded && Mathf.Approximately(rb.velocity.y, 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        gameObject.GetComponent<SpriteRenderer>().color = lives > 1 ? Color.white : new Color(1, 0.5f, 0.5f); //Operador ternario
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedButton"))
        {
            Destroy(collision.gameObject);
            Destroy(redWall);
        }
        if (collision.gameObject.CompareTag("YellowButton"))
        {
            Destroy(collision.gameObject);
            Destroy(yellowWall);
        }
        if (collision.gameObject.CompareTag("GreenButton"))
        {
            Destroy(collision.gameObject);
            Destroy(greenWall);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            isLevelCompleted = true;
            Invoke("LoadLevelsScene", 2f);
        }
    }

    void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }
}
