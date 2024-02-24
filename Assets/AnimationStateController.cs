using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    // sets inspector component.
    Animator Animator;
    int isWalkingHash;
    int isRunningHash;
    float Velocity = 0.0f;
    int VelocityHash;
    public float acceleration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        // String to hash increases performance, better data value i've heard. basically changes the "" into hash.
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        VelocityHash = Animator.StringToHash("Velocity");

    } 

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Animator.GetBool("isRunning"); //
        bool isWalking = Animator.GetBool(isWalkingHash); // Makes it so it only calls the function once.
        bool forwardPressed = Input.GetKey("w"); //Similar to above.
        bool runPressed = Input.GetKey("left shift"); //




        // if player is pressing W key
        if (!isWalking && forwardPressed)
        {
            //then set isWalking to true
            Animator.SetBool(isWalkingHash, true);
        }
        // if player is not pressing W key
        if (isWalking && !forwardPressed)
        {
            Animator.SetBool(isWalkingHash, false);
        }
        // If player is not running, and both keys are pressed, set true
        if (!isRunning && (runPressed && forwardPressed))
        {
            Animator.SetBool(isRunningHash, true);
        }
        // If player is running, and either keys released, set false.
        if (isRunning && (!runPressed || !forwardPressed))
        {
            Animator.SetBool(isRunningHash, false);

        }
        // Velocity Functions
        if (forwardPressed)
        {
            Velocity += Time.deltaTime * acceleration;
        }
        Animator.SetFloat(VelocityHash, Velocity);

    }
}
