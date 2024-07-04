using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    //Descripción: Proyectil que dispara el jefe, puede hacer 1 o 2 de daño.

    public float Speed = 10f;
    public int Damage = 1;
    public float CriticalMultiplier = 2;

    private Transform player;
    private CVector2 direction; //Vector creado en CVector2

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
            CVector2 playerPosition = new CVector2(player.position.x, player.position.y);
            CVector2 projectilePosition = new CVector2(transform.position.x, transform.position.y);

            direction = CVector2.Normaliza(playerPosition - projectilePosition); //Vector: Normalización (Magnitud incluído en CVector2) y Aritmética
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        //Moverse a la ubicación obtenida del jugador
        Vector3 movement = new Vector3(direction.X, direction.Y, 0f) * Speed * Time.deltaTime;
        transform.Translate(movement);

        //Destruir el proyectil si está muy lejos del jugador

        CVector2 currentPosition = new CVector2(transform.position.x, transform.position.y);
        CVector2 playerPosition = new CVector2(player.position.x, player.position.y);

        if (CVector2.Distancia(currentPosition,playerPosition) > 20f) //Vector: Distancia
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
            float x = myPlayer.Lives;
            CheckPlayerLife(x);
            CameraShakeController.instance.ShakeCamera(0.5f, 0.1f);
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
