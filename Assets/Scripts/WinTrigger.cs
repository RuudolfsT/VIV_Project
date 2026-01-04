using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;   // UI to show when player wins

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show UI
            winPanel.SetActive(true);

            // Stop the game
            Time.timeScale = 0f;

            // Unlock cursor to click UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
