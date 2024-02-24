using Unity.Properties;
using UnityEngine;

//-X In the Transistion going from Idle to the Blend Tree for Walking and Running
//  Had Exit Time has been toggled off to allow smoother feel of player going out from idle to walk animation instantly



public class AnimationStateController : MonoBehaviour
{
    // sets inspector component.
    Animator animator;
    //int isWalkingHash;
    //int isRunningHash;
    float velocity = 0.0f;
    int velocityHash;
    public float acceleration = 0.1f;
    public bool runPressed;
    public bool forwardPressed;
    public float outOfRunRate;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //String to hash increases performance, better data value i've heard. basically changes the "" into hash.
        //isWalkingHash = Animator.StringToHash("isWalking");
        //isRunningHash = Animator.StringToHash("isRunning");
        velocityHash = Animator.StringToHash("Velocity");

    } 

    // Update is called once per frame
    void Update()
    {
        //bool isRunning = Animator.GetBool("isRunning"); //
        //bool isWalking = Animator.GetBool(isWalkingHash); // Makes it so it only calls the function once.
        forwardPressed = Input.GetKey("w"); //Similar to above.
        runPressed = Input.GetKey("left shift"); //

/*
        //// if player is pressing W key
        //if (!isWalking && forwardPressed)
        //{
        //    //then set isWalking to true
        //    Animator.SetBool(isWalkingHash, true);
        //}
        //// if player is not pressing W key
        //if (isWalking && !forwardPressed)
        //{
        //    Animator.SetBool(isWalkingHash, false);
        //}
        //// If player is not running, and both keys are pressed, set true
        //if (!isRunning && (runPressed && forwardPressed))
        //{
        //    Animator.SetBool(isRunningHash, true);
        //}
        //// If player is running, and either keys released, set false.
        //if (isRunning && (!runPressed || !forwardPressed))
        //{
        //    Animator.SetBool(isRunningHash, false);

        //}

*/

        // Velocity Functions
            //-X Checking Only if Player has pressed forward
        if (forwardPressed)
        {
            //-X If player also addiontally presses run key, multiply accelearation by 2 to speed up faster
            if(runPressed)
            {
                velocity = Mathf.Clamp(velocity + Time.deltaTime * (acceleration * 2), 0, 1);
            }
            else//-X Else Player is walking
            {
                /*-X This is a short-hand if statement. It is ordered as such
                 *  conditional(whether true or false) ? if true, this is assigned value : else false, this is assigned value
                 *  Ex. int x = 10 > 5 ? 20 : 0 ------- x will be assigned 20
                 *  
                 *  Checking if velocity is greater than the threshold(0.5) of running in the Blend Tree between walking and running
                 *      If over threshold, lerp velocity value so play slows down from run to walk
                 *      If player underthreshold, then just clamp velocity value to threshold(0.5) of walk and run blend tree
                 *      outOfRunRate is used as a way to have player slow down through lerp quicker.
                 *      --Note Lerps goes between first and second argument by a time of third arugment, so it gives an inbetween number
                 *          Bigger values in third arugment make going from first to second quicker;
                */
                velocity = velocity > 0.5? Mathf.Lerp(velocity, 0.5f, outOfRunRate * Time.deltaTime):
                    Mathf.Clamp(velocity + Time.deltaTime * acceleration, 0, 0.5f);
            }

        }
        else
        {
            //-X Decelerate player if forward pressed is not held. This acceleration is multiplied by a factor of 3 to make this quicker
            velocity = Mathf.Clamp(velocity - Time.deltaTime * (acceleration * 3),0, 1);
        }

        //Set velocity value for blend in animator
        /*-X Note about how Transitions Happen:
         * Previous hashes, isRunning and isWalking are uneccesary, as animation state graph connects idel to a blend tree
         * Said blend tree contains walking and runnning animations. All that needs to be done is adjust the value of the velocity.
         * This velocity value is equivalnt to a weight value for selection. Therefore, adjusting the velocity will display more the
         *  desired animation
         */
        animator.SetFloat(velocityHash, velocity);

    }
}
