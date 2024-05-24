using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public int Damage = 1;
    public float CriticalMultiplier = 2;

    private Transform player;
    private Vector3 direction;

    private enum BulletType { Default, Critical}; //Enumerador
    private BulletType bulletType;

    void Start()
    {
        bulletType = (BulletType)Random.Range(0, 2);
        switch (bulletType) 
        {
            case BulletType.Default:
                break;
            case BulletType.Critical:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
        
        //Buscar ubicación del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        //Moverse a la ubicación obtenida del jugador
        transform.Translate(direction * Speed * Time.deltaTime);
        //Destruir el proyectil si está muy lejos del jugador
        if (Vector3.Distance(transform.position, player.position) > 20f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (bulletType) //Aplicar daño dependiendo de lo obtenido en enumarador
            {
                case BulletType.Default:
                    ApplyDamage(Damage, collision);
                    break;
                case BulletType.Critical:
                    ApplyDamage(Damage, CriticalMultiplier, collision);
                    break;
            }
            Player myPlayer = collision.gameObject.GetComponent<Player>(); //Acceder a propiedades del jugador
            /*myPlayer.Lives = myPlayer.Lives - Damage;*/
            float x = myPlayer.Lives;
            CheckPlayerLife(x);
            Destroy(gameObject);
        }
    }

    private void ApplyDamage(int damage, Collision2D player) //Overloading 
    {
        Player myPlayer = player.gameObject.GetComponent<Player>();
        myPlayer.Lives = myPlayer.Lives - damage;
    }

    private void ApplyDamage(int damage, float criticalMultiplier, Collision2D player) //Overloading
    {
        Player myPlayer = player.gameObject.GetComponent<Player>();
        float criticalDamage = damage * criticalMultiplier;
        myPlayer.Lives = myPlayer.Lives - criticalDamage;
    }

    private void CheckPlayerLife(float playerLife)
    {
        if (playerLife <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
