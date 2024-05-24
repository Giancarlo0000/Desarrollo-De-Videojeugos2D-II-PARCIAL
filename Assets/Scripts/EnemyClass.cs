using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyClass : MonoBehaviour
{
    //Herencia - Clase enemigo que se usará para los enemigos normales y el jefe 
    [SerializeField] protected float speed = 2;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float followRange = 2;

    protected Transform player;
    protected Vector3 scale;
    protected bool canMove = true;
    protected bool isFollowingPlayer = false;

    private void Awake()
    {
        scale = transform.localScale;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) <= followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            isFollowingPlayer = false; // Si el jugador está fuera del rango, deja de seguirlo
        }

        float distanciaDeJugador = Vector3.Distance(transform.position, player.position);

        if (distanciaDeJugador < followRange)
        {
            Vector3 direccion = player.position - transform.position;
            if (Vector3.Dot(direccion, transform.right) < 0)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player myPlayer = collision.gameObject.GetComponent<Player>(); //Acceder a propiedades del jugador
            myPlayer.Lives = myPlayer.Lives - damage;
            float x = myPlayer.Lives;
            if (x <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Destroy(gameObject);
        }
    }
}
