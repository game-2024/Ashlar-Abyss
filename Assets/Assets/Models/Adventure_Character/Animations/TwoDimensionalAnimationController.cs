using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationController : MonoBehaviour
{
    Animator animator;
    float VelocityZ = 0.0f;
    float VelocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float MaximumWalkVelocity = 0.5f;
    public float MaximumRunVelocity = 2.0f;
 // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
 //Function handles Acceleration and Deceleration
    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed,bool runPressed, float CurrentMaxVelocity)
    {
        //If statements that increase velocity longer holds key.
        if (forwardPressed && VelocityZ < CurrentMaxVelocity)
        {
            VelocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && VelocityX > -CurrentMaxVelocity)
        {
            VelocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && VelocityX < CurrentMaxVelocity)
        {
            VelocityX += Time.deltaTime * acceleration;
        }
        //Deceleration statements and adding limits
        if (!forwardPressed && VelocityZ > 0.0f)
        {
            VelocityZ -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && VelocityZ < 0.0f)
        {
            VelocityZ = 0.0f;
        }
        //Deceleration for Left and Right
        if (!rightPressed && VelocityX > 0.0f) // if not right press and greater than 0, decrease
        {
            VelocityX -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && VelocityX < 0.0f) //if not left press and greater than 0, decrease
        {
            VelocityX += Time.deltaTime * deceleration;
        }
    }
    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float CurrentMaxVelocity)
    {
        //Final reset for X, needs additional parameters for if velocity gets bugged, failsafe.
        if (!leftPressed && !rightPressed && VelocityX != 0.0f && (VelocityX > -0.05f && VelocityX < 0.05f))
        {
            VelocityX = 0.0f;
        }
        //Lock Forward 
        if (forwardPressed && runPressed && VelocityZ > CurrentMaxVelocity)
        {
            VelocityZ = CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (forwardPressed && VelocityZ > CurrentMaxVelocity)
        {
            VelocityZ -= Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (VelocityZ > CurrentMaxVelocity && VelocityZ < (CurrentMaxVelocity + 0.05))
            {
                VelocityZ = CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (forwardPressed && VelocityZ < CurrentMaxVelocity && VelocityZ > (CurrentMaxVelocity - 0.05f))
        {
            VelocityZ = CurrentMaxVelocity;
        }

        //Lock Left *All values are reversed since X axis is negative.*
        if (leftPressed && runPressed && VelocityX < -CurrentMaxVelocity)
        {
            VelocityX = -CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (leftPressed && VelocityX < -CurrentMaxVelocity)
        {
            VelocityX += Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (VelocityX < -CurrentMaxVelocity && VelocityX > (-CurrentMaxVelocity - 0.05f))
            {
                VelocityX = -CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (leftPressed && VelocityX > -CurrentMaxVelocity && VelocityX < (-CurrentMaxVelocity + 0.05f))
        {
            VelocityX = -CurrentMaxVelocity;
        }
        //Lock Right 
        if (rightPressed && runPressed && VelocityX > CurrentMaxVelocity)
        {
            VelocityX = CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (rightPressed && VelocityX > CurrentMaxVelocity)
        {
            VelocityX -= Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (VelocityX > CurrentMaxVelocity && VelocityX < (CurrentMaxVelocity + 0.05f))
            {
                VelocityX = CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (rightPressed && VelocityX < CurrentMaxVelocity && VelocityX > (CurrentMaxVelocity - 0.05f))
        {
            VelocityX = CurrentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
 //Get key input from player
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        // Set Current Max Velocity, used for running
        float CurrentMaxVelocity = runPressed ? MaximumRunVelocity : MaximumWalkVelocity; //statement sets CurrentMaxVelocity to one of 2 values if true or false.
        //Call functions to handle all changes in velocity
        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, CurrentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, CurrentMaxVelocity);
        //Calls reference from other local variables.
        animator.SetFloat("VelocityZ", VelocityZ);
        animator.SetFloat("VelocityX", VelocityX);
    }
}
