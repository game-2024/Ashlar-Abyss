using UnityEngine;
using UnityEngine.Events;

public class TwoDimensionalAnimationController : MonoBehaviour
{
    Animator animator;
    float AnimWeightZ = 0.0f;
    float AnimWeightX = 0.0f;
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float MaximumWalkVelocity = 0.5f;
    public float MaximumRunVelocity = 1.0f;


    [SerializeField][Range(0.1f, 1f)] float AnimatorSpeed = 0.5f;

    [SerializeField] private UnityEvent OnAttackAnimSwingStart;
    [SerializeField] private UnityEvent OnSwordComeBack;
    [SerializeField] private UnityEvent OnAttackAnimSwingEnd;


 
 // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = AnimatorSpeed;

    }
 //Function handles Acceleration and Deceleration
    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed,bool runPressed, float CurrentMaxVelocity)
    {
        //If statements that increase velocity longer holds key.
        if (forwardPressed && AnimWeightZ < CurrentMaxVelocity)
        {
            AnimWeightZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && AnimWeightX > -CurrentMaxVelocity)
        {
            AnimWeightX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && AnimWeightX < CurrentMaxVelocity)
        {
            AnimWeightX += Time.deltaTime * acceleration;
        }
        //Deceleration statements and adding limits
        if (!forwardPressed && AnimWeightZ > 0.0f)
        {
            AnimWeightZ -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && AnimWeightZ < 0.0f)
        {
            AnimWeightZ = 0.0f;
        }
        //Deceleration for Left and Right
        if (!rightPressed && AnimWeightX > 0.0f) // if not right press and greater than 0, decrease
        {
            AnimWeightX -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && AnimWeightX < 0.0f) //if not left press and greater than 0, decrease
        {
            AnimWeightX += Time.deltaTime * deceleration;
        }
    }
    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float CurrentMaxVelocity)
    {
        //Final reset for X, needs additional parameters for if velocity gets bugged, failsafe.
        if (!leftPressed && !rightPressed && AnimWeightX != 0.0f && (AnimWeightX > -0.05f && AnimWeightX < 0.05f))
        {
            AnimWeightX = 0.0f;
        }
        //Lock Forward 
        if (forwardPressed && runPressed && AnimWeightZ > CurrentMaxVelocity)
        {
            AnimWeightZ = CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (forwardPressed && AnimWeightZ > CurrentMaxVelocity)
        {
            AnimWeightZ -= Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (AnimWeightZ > CurrentMaxVelocity && AnimWeightZ < (CurrentMaxVelocity + 0.05))
            {
                AnimWeightZ = CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (forwardPressed && AnimWeightZ < CurrentMaxVelocity && AnimWeightZ > (CurrentMaxVelocity - 0.05f))
        {
            AnimWeightZ = CurrentMaxVelocity;
        }

        //Lock Left *All values are reversed since X axis is negative.*
        if (leftPressed && runPressed && AnimWeightX < -CurrentMaxVelocity)
        {
            AnimWeightX = -CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (leftPressed && AnimWeightX < -CurrentMaxVelocity)
        {
            AnimWeightX += Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (AnimWeightX < -CurrentMaxVelocity && AnimWeightX > (-CurrentMaxVelocity - 0.05f))
            {
                AnimWeightX = -CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (leftPressed && AnimWeightX > -CurrentMaxVelocity && AnimWeightX < (-CurrentMaxVelocity + 0.05f))
        {
            AnimWeightX = -CurrentMaxVelocity;
        }
        //Lock Right 
        if (rightPressed && runPressed && AnimWeightX > CurrentMaxVelocity)
        {
            AnimWeightX = CurrentMaxVelocity;
        }
        //Decelerate to maximum walk velocity
        else if (rightPressed && AnimWeightX > CurrentMaxVelocity)
        {
            AnimWeightX -= Time.deltaTime * deceleration;
            //Rounds to CurrentMaxVelocity if within decel offset.
            if (AnimWeightX > CurrentMaxVelocity && AnimWeightX < (CurrentMaxVelocity + 0.05f))
            {
                AnimWeightX = CurrentMaxVelocity;
            }
        }
        //rounds to CurrentMaxVelocity if within accel offset.
        else if (rightPressed && AnimWeightX < CurrentMaxVelocity && AnimWeightX > (CurrentMaxVelocity - 0.05f))
        {
            AnimWeightX = CurrentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get key input from player
        //bool forwardPressed = Input.GetKey("w");
        //bool leftPressed = Input.GetKey("a");
        //bool rightPressed = Input.GetKey("d");
        //bool runPressed = Input.GetKey("left shift");
        //// Set Current Max Velocity, used for running
        //float CurrentMaxVelocity = runPressed ? MaximumRunVelocity : MaximumWalkVelocity; //statement sets CurrentMaxVelocity to one of 2 values if true or false.
        ////Call functions to handle all changes in velocity
        //ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, CurrentMaxVelocity);
        //LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, CurrentMaxVelocity);
        //Calls reference from other local variables.


        float forwardInputAxis = Input.GetAxis("Vertical") >= 0.1 ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical") * 0.5f;

        animator.SetFloat("AnimWeightZ", forwardInputAxis);
        animator.SetFloat("AnimWeightX", Input.GetAxis("Horizontal"));

    }



    public void OnSwordStartSwing()
    {
        OnAttackAnimSwingStart?.Invoke();
    }

    public void OnSwordComingBack()
    {
        OnSwordComeBack?.Invoke();
    }

    public void OnSwordAttackAnimFinish()
    {
        OnAttackAnimSwingEnd?.Invoke();
    }

    public void TriggerAttackAnimState()
    {
        animator.SetTrigger("isAttacking");
    }



}
