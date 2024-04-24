using UnityEngine;
using UnityEngine.SceneManagement;


public class WinScreen : MonoBehaviour
{



    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }




    public void ReturnToMainMenuPressed()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

}
