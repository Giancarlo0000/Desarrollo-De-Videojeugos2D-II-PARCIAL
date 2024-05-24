using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : EnemyClass
{
    public Transform[] patrolPoints; // Puntos entre los cuales el enemigo patrullará

    private int currentPatrolIndex = 0; // Índice del punto de patrulla actual
    //private bool isFollowingPlayer = false; // Indica si el enemigo está siguiendo al jugador


    void Update()
    {
        if (!isFollowingPlayer)
        {
            Patrol(); // Si no sigue al jugador, realiza la patrulla
        }
        else
        {
            FollowPlayer(); // Si sigue al jugador, lo persigue
        }

        CheckDistance();
    }

    IEnumerator WaitSeconds() //Corrutina
    {
        canMove = false;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        yield return new WaitForSeconds(2f);
        canMove = true;
        if (currentPatrolIndex == 0)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if (currentPatrolIndex == 1)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    void Patrol()
    {
        // Mueve hacia el punto de patrulla actual
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
        }
        // Si llega al punto de patrulla, avanza al siguiente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            StartCoroutine(WaitSeconds());
            //currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        if (canMove)
        {
            if (currentPatrolIndex == 0)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            if (currentPatrolIndex == 1)
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
    }

    void FollowPlayer()
    {
        Attack();
    }

    void CheckDistance()
    {
        if (Vector2.Distance(transform.position, player.position) <= followRange)
        {
            isFollowingPlayer = true;
        }
    }
}