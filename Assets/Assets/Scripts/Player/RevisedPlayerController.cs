using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class RevisedPlayerMovement : MonoBehaviour
{

    Transform PlayerCamTransformReference;
    [SerializeField] private float Speed;
    //[SerializeField] private Rigidbody playerRB;
    [SerializeField] private CharacterController PlayerCharacterController;
    public bool togglecamera;
    public bool toggleMove;



    Vector3 InputAxis;
    float forwardMovementInputAxis;
    float horizontalMovementInputAxis;


    private float TargetRotation;
    private float _rotationVelocity;
    [SerializeField] private float SmoothingTime;



    private void Start()
    {
        PlayerCamTransformReference = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }



    private void Update()
    {

        InputAxis = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        
        //Player Movement and Rotations
        HandleMovement();
        HandlePlayerRotation();





    }


    #region Player Movement And Rotation Functions

    private void HandleMovement()
    {
        Vector3 forwardCamRelativeMovement = PlayerCamTransformReference.forward;
        Vector3 horizontalCamRelativeMovement = PlayerCamTransformReference.right;

        forwardCamRelativeMovement.y = 0f;
        horizontalCamRelativeMovement.y = 0f;

        forwardCamRelativeMovement.Normalize();
        horizontalCamRelativeMovement.Normalize();


        Vector3 finalInputCamRelativeMove = forwardCamRelativeMovement * InputAxis.z + horizontalCamRelativeMovement * InputAxis.x;
        finalInputCamRelativeMove.y = 0f;


        PlayerCharacterController.Move(Speed * Time.deltaTime * finalInputCamRelativeMove);

    }

    private void HandlePlayerRotation()
    {
        if (InputAxis != Vector3.zero)
        {

            TargetRotation = Mathf.Atan2(InputAxis.x, InputAxis.z) * Mathf.Rad2Deg + PlayerCamTransformReference.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(PlayerCharacterController.transform.eulerAngles.y, TargetRotation,
                                                    ref _rotationVelocity, SmoothingTime);

            PlayerCharacterController.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
    }

    #endregion


    #region Old Attempt

    //// Start is called before the first frame update
    //void Start()
    //{
    //    PlayerCamTransformReference = FindObjectOfType<CinemachineVirtualCamera>().transform;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //User Movement Input
    //    forwardMovementInputAxis = Input.GetAxis("Vertical");
    //    horizontalMovementInputAxis = Input.GetAxis("Horizontal");

    //}



    //private void FixedUpdate()
    //{
    //    if (toggleMove)
    //    {
    //        MovementRealtiveToCamera();
    //    }

    //    if (togglecamera)
    //        RotateWithInputRealtiveToCamera();
    //}



    //private void MovementRealtiveToCamera()
    //{

    //    Vector3 newFwd = forwardMovementInputAxis * PlayerCamTransformReference.forward;
    //    Vector3 newRight = horizontalMovementInputAxis * PlayerCamTransformReference.right;

    //    Vector3 input = new Vector3(horizontalMovementInputAxis, 0f, forwardMovementInputAxis).normalized;

    //    //Vector3 test = playerRB.transform.TransformDirection(input) * Speed;

    //    //playerRB.velocity = new Vector3(input.x, playerRB.velocity.y, input.z);

    //    //playerRB.MovePosition(new Vector3(input.x, playerRB.velocity.y, input.z));

    //    playerRB.AddRelativeForce(input * Speed * Time.deltaTime, ForceMode.Force);


    //    //Vector3 camRelative = (newFwd + newRight).normalized * Speed;

    //    //camRelative.y = 0f;
    //    //playerRB.velocity = camRelative;


    //    /*   //Vector3 camRelativeMovementNormals = (PlayerCamTransformReference.forward + PlayerCamTransformReference.right);
    //       //camRelativeMovementNormals.y = 0f;

    //       //camRelativeMovementNormals.x *= horizontalMovementInputAxis;
    //       //camRelativeMovementNormals.z *= forwardMovementInputAxis;


    //       //Vector3 finalMovement = camRelativeMovementNormals * Speed;
    //       //playerRB.velocity = finalMovement;
    //    */
    //}

    //private float turnVelo;

    //private void RotateWithInputRealtiveToCamera()
    //{
    //    Vector3 charRotation;

    //    charRotation.x = horizontalMovementInputAxis;
    //    charRotation.y = 0f;
    //    charRotation.z = forwardMovementInputAxis;


    //    Quaternion currRotation = playerRB.transform.rotation;

    //    if (charRotation != Vector3.zero)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(charRotation);
    //        playerRB.transform.rotation = Quaternion.Slerp(currRotation, targetRotation, 1f);
    //    }


    //    //Vector3 preDir = playerRB.transform.forward;

    //    //Vector3 camForwardTranform = (PlayerCamTransformReference.forward + PlayerCamTransformReference.right).normalized;

    //    //camForwardTranform.x *= horizontalMovementInputAxis;
    //    //camForwardTranform.y = 0f;
    //    //camForwardTranform.z *= forwardMovementInputAxis;

    //    //playerRB.transform.rotation = Quaternion.LookRotation(camForwardTranform,Vector3.up);

    //    //if (camForwardTranform != Vector3.zero)
    //    //{
    //    //    //playerRB.transform.forward = camForwardTranform;
    //    //}
    //    //else
    //    //{
    //    //    //playerRB.transform.forward = preDir;
    //    //}

    //    //Vector3 lastFacingPostition = playerRB.transform.forward;
    //    //Debug.Log(playerRB.transform.forward);


    //    //Vector3 newFwd = forwardMovementInputAxis * PlayerCamTransformReference.forward;
    //    //Vector3 newRight = horizontalMovementInputAxis * PlayerCamTransformReference.right;

    //    //Vector3 camRelative = newFwd + newRight;

    //    //camRelative.y = 0f;
    //    //camRelative.Normalize();
    //    //if (camRelative != Vector3.zero)
    //    //{
    //    //    playerRB.transform.forward = camRelative;
    //    //}
    //    //else
    //    //{
    //    //    playerRB.transform.forward = lastFacingPostition;
    //    //}



    //    /*
    //        Vector3 playerCamRealtiveRotation = new(PlayerCamTransformReference.right.x * horizontalMovementInputAxis, 0f,
    //                                             PlayerCamTransformReference.forward.z * forwardMovementInputAxis);

    //        playerCamRealtiveRotation.Normalize();

    //        if (playerCamRealtiveRotation != Vector3.zero)
    //        {
    //            playerRB.transform.forward = playerCamRealtiveRotation;
    //            //Debug.Log(playerRB.transform.forward);
    //        }
    //    */



    //}


    #endregion

}
