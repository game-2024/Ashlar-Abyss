using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Gets refernce to player object where said object has a Speed variable
    [SerializeField] PlayerStatsScript player;

    //Reference to Lantern object for player to be able to toggle lantern
    [SerializeField] private LanternScript lantern;

    //Player and Diesel Variables for changing variables
    //FOR TESTING PURPOSES ONLY
    [SerializeField] private PlayerHealthManagerScript healthManager;
    [SerializeField] private DieselManagerScript dieselManager;

    //Private Speed variable gotten from PlayerStatsScript to allow player movement
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Speed = player.GetPlayerSpeed();
    }

    void Update()
    {
        #region Temp Movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.Translate(new Vector3(0, 0, 1) * Speed * Time.deltaTime, Space.World);
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.Translate(new Vector3(0, 0, -1) * Speed * Time.deltaTime, Space.World);
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Move the Rigidbody right constantly at the speed you define (the blue arrow axis in Scene view)  
            player.transform.Translate(new Vector3(1, 0, 0) * Speed * Time.deltaTime, Space.World);
            player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Move the Rigidbody left constantly at the speed you define (the blue arrow axis in Scene view)
            player.transform.Translate(new Vector3(-1, 0, 0) * Speed * Time.deltaTime, Space.World);
            player.transform.rotation = Quaternion.Euler(0f, 270, 0f);
        }
        #endregion

        #region Toggle Lantern ON or OFF
        if (Input.GetKeyDown(KeyCode.J))
        {
            lantern.ToggleLantern();
        }
        #endregion

        #region Test Health Adjustments Manually

        //Heal Player
        if (Input.GetKeyDown(KeyCode.Y))
        {
            healthManager.HealPlayer();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            healthManager.DamagePlayer(healthManager.damage);
        }

        #endregion

        #region Restore Diesel Fuel Manually

            if (Input.GetKeyDown(KeyCode.H))
            {
                dieselManager.CurrentDiesel = dieselManager.MaxDiesel;
            }

        #endregion

    }

}

