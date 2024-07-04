using UnityEngine;

public class EnemyBee : EnemyClass
{
    //Descripción: Enemigo que se queda volando en el aire, dispara un proyectil hacia abajo cuando el jugador está cerca

    private Animator anim;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float attackRange = 5f;
    [SerializeField] private float firingFrequency = 2f;
    private float lastShootTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        lastShootTime = Time.time;
    }

    private void Update()
    {
        Attack();
        Shoot();
    }

    private void Shoot()
    {
        float HorizontalDistancePlayer = Mathf.Abs(transform.position.x - player.position.x);


        if (Time.time - lastShootTime > firingFrequency && HorizontalDistancePlayer < attackRange)
        {
            Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y - 0.6f, 0f), transform.rotation);
            lastShootTime = Time.time;
        }
    }
}
