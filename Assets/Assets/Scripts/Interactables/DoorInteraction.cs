using UnityEngine;
using UnityEngine.SceneManagement;



public class DoorInteraction : MonoBehaviour
{

    public void DoorInteracted()
    {
        SceneManager.LoadScene("FinishScene", LoadSceneMode.Single);
    }


}
