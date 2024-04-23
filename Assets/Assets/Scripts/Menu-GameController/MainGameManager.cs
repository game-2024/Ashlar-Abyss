using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{

    [SerializeField] PlayerHealthManagerScript PlayerHealthManager;
    [SerializeField] DieselManagerScript DieselManager;

    private void Start()
    {
        PlayerHealthManager.InitializeHealthManager();
        DieselManager.InitializeDieselManager();

        PlayerHealthManager.OnPlayerDied += PlayerDied;

    }


    private void OnDisable()
    {
        PlayerHealthManager.OnPlayerDied -= PlayerDied;
    }



    private void PlayerDied()
    {
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            PlayerHealthManager.DamagePlayer(500f);
        }
    }



}
