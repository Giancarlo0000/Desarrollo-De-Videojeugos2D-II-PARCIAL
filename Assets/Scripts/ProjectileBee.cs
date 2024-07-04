using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileBee : MonoBehaviour
{
    //Descripción: Proyectil que lanza la abeja, solo va hacia abajo

    public float Speed = 10f;
    public int Damage = 1;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(Damage, collision);
            Player myPlayer = collision.gameObject.GetComponent<Player>();
            float x = myPlayer.Lives;
            CheckPlayerLife(x);
            CameraShakeController.instance.ShakeCamera(0.5f, 0.1f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamage(int damage, Collision2D player)
    {
        Player myPlayer = player.gameObject.GetComponent<Player>();
        myPlayer.Lives = myPlayer.Lives - damage;
    }

    private void CheckPlayerLife(float playerLife)
    {
        if (playerLife <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
