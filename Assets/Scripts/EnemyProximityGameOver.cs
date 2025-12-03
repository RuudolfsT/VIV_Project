using UnityEngine;

public class EnemyProximityGameOver : MonoBehaviour
{
    public Transform player;         // player transform
    public Transform enemy;          // AI enemy transform
    public float triggerDistance = 2.0f;
    public CameraController cam;     // reference to your camera controller
    public AIBlink aiBlinkScript;

    private bool gameOverTriggered = false;
    public GameObject restartButton;
    void Update()
    {
        if (gameOverTriggered) return;

        float dist = Vector3.Distance(player.position, enemy.position);

        if (dist <= triggerDistance)
        {
            gameOverTriggered = true;
            TriggerGameOver();
        }
    }

    void Start()
    {
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    void TriggerGameOver()
    {
        if (aiBlinkScript != null)
        {
            aiBlinkScript.enabled = false;
        }

        if (aiBlinkScript != null && aiBlinkScript.objectVisibleFeature != null)
        {
            aiBlinkScript.objectVisibleFeature.SetActive(true);
        }

        restartButton.SetActive(true);

        MeshRenderer renderer = enemy.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        Vector3 lookDirection = player.position - enemy.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            enemy.rotation = Quaternion.LookRotation(lookDirection);
        }


        Debug.Log("GAME OVER: Enemy caught the player!");
        cam.FocusOnEnemy(enemy);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
