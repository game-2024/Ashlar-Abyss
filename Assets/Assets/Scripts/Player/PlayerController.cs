using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerStatsScript player;

    [SerializeField] private LanternScript lantern;

    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Speed = player.GetPlayerSpeed();
    }

    void Update()
    {
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


        if (Input.GetKeyDown(KeyCode.J))
        {
            lantern.ToggleLantern();
        }



    }


}

