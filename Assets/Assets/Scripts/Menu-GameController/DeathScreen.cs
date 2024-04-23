using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToMainMenuButtonPressed()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
