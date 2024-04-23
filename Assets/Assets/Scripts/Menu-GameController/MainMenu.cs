using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("PrototypeLevelScene", LoadSceneMode.Single);
    }


    public void CloseGameButtonPressed()
    {
        Application.Quit();
    }


}
