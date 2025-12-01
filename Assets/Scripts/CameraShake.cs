using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform enemy;
    public float triggerDistance = 5f;   // Start shaking when enemy is within this distance
    public float shakeStrength = 0.1f;   // How much to shake
    public float shakeSpeed = 20f;       // How fast the shake is

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, enemy.position);

        if (distance < triggerDistance)
        {
            float shakeAmount = shakeStrength * (1f - (distance / triggerDistance));
            Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
            transform.localPosition = originalPos + shakeOffset;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalPos,
                Time.deltaTime * 5f
            );
        }
    }
}
