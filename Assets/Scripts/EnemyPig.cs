using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyClass
{
    //Descripcion: Enemigo que corre muy rápido, pero se detiene al llegar aL RestAreaPoint asignado, solo hace 1 de daño.
    private CVector2 restAreaPosition;
    [SerializeField] private Transform RestAreaPoint;
    private Animator anim;
    private void Start()
    {
        restAreaPosition = new CVector2(RestAreaPoint.position.x, RestAreaPoint.position.y);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (RestAreaPoint != null)
        {
            Attack();
        }

        CVector2 pigPoistion = new CVector2(transform.position.x, transform.position.y);

        if (Mathf.Abs(pigPoistion.X - restAreaPosition.X) <= 0.5f && Mathf.Abs(pigPoistion.Y - restAreaPosition.Y) <= 0.35f) //Comparacion de Vectores, similar al de CVector2 pero com más margen
        {
            CalmDown();
        }
    }

    private void CalmDown()
    {
        speed = 0f;
        anim.SetBool("isUpset", false);
    }
}
