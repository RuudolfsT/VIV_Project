using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform normalTarget;  // player or gameplay target
    public float smoothSpeed = 3f;

    private Transform gameOverTarget = null;
    public Vector3 focusOffset = new Vector3(0, 1.5f, 0); // adjust Y to match AI height

    void LateUpdate()
    {
        if (gameOverTarget != null)
        {
            Vector3 targetPosition = gameOverTarget.position + focusOffset;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition + gameOverTarget.forward * -3 + Vector3.up * 2,
                Time.deltaTime * smoothSpeed
            );

            transform.LookAt(targetPosition);
        }
        //else
        //{
        //    // Normal camera behavior
        //    transform.position = Vector3.Lerp(
        //        transform.position,
        //        normalTarget.position + new Vector3(0, 5, -7),
        //        Time.deltaTime * smoothSpeed
        //    );
        //    transform.LookAt(normalTarget);
        //}
    }

    public void FocusOnEnemy(Transform enemy)
    {
        gameOverTarget = enemy;
    }
}
