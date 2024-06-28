using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : EnemyClass
{
    //Descripcion: Enemigo que corre muy rápido, pero se detiene al llegar aL RestAreaPoint asignado, solo hace 1 de daño.
    private GameObject[] restAreaPoints;
    private Animator anim;
    [SerializeField] private float stopDistance = 0.5f;

    private void Start()
    {
        restAreaPoints = GameObject.FindGameObjectsWithTag("RestAreaPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();

        foreach (GameObject restAreaPoint in restAreaPoints)
        {
            float distanceToRestArea = Vector2.Distance(transform.position, restAreaPoint.transform.position);
            if (distanceToRestArea <= stopDistance)
            {
                CalmDown();
                break;
            }
        }
    }

    private void CalmDown()
    {
        speed = 0f;
        anim.SetBool("isUpset", false);
    }
}
