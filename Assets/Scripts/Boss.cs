using UnityEngine;

public class Boss : EnemyClass
{
    public float firingFrequency = 2f;

    public Transform shootingPoint;
    public GameObject projectilePrefab;

    private float lastShootTime;

    private void Awake() => scale = transform.localScale;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastShootTime = Time.time;
    }

    void Update()
    {
        Attack();
    }
    //Polimorfismo - Agregar ataque con proyectil
    public override void Attack() //Sobrescribir método
    {
        base.Attack();
        if (Time.time - lastShootTime > firingFrequency && Vector2.Distance(transform.position, player.position) <= followRange - 3)
        {
            Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            lastShootTime = Time.time;
        }
    }
}
