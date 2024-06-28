using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController instance;
    private CameraShake cameraShake;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cameraShake = GetComponent<CameraShake>();
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(duration, magnitude));
        }
    }
}
