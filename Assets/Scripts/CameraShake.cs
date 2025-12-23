using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform enemy;
    public float triggerDistance = 5f;
    public float shakeStrength = 0.1f;
    public float shakeSpeed = 20f;

    [Header("Audio")]
    public AudioSource proximityAudio;
    public float maxVolume = 1f;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;

        if (proximityAudio != null)
        {
            proximityAudio.loop = true;
            proximityAudio.playOnAwake = false;
            proximityAudio.volume = 0f;
        }
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

        // Audio logic
        if (proximityAudio != null)
        {
            if (distance < triggerDistance)
            {
                if (!proximityAudio.isPlaying)
                    proximityAudio.Play();

                float volume = 1f - (distance / triggerDistance);
                proximityAudio.volume = Mathf.Clamp01(volume) * maxVolume;
            }
            else
            {
                proximityAudio.volume = Mathf.Lerp(
                    proximityAudio.volume,
                    0f,
                    Time.deltaTime * 5f
                );

                if (proximityAudio.volume <= 0.01f && proximityAudio.isPlaying)
                    proximityAudio.Stop();
            }
        }
    }
}
