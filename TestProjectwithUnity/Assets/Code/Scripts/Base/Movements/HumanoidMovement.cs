using UnityEngine;

public class HumanoidMovement : ExtendedCustomMonoBehavior
{
    public CamMovement cam;

    private Animator animator;

    enum CharacterState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
    }

    private CharacterState characterstate_var;

    public float walkspeed = 5.0f;
    public float runspeed = 10.0f;

    public float speedSmoothing = 10.0f;
    public float rotateSpeed = 500.0f;

    private Vector3 moveDirection = Vector3.zero;

    public float moveSpeed = 0.0f;

    private PlayerManager myPlayerController;
    private WeaponController shooter;
    private KeyboardInput default_input;

    public float horz;
    public float vert;

    private CharacterController controller;

    //Temporary
    private bool riflepause = false;

    void Awake()
    {   
        moveDirection = transform.TransformDirection(Vector3.forward);

        animator = GetComponent<Animator>();
        characterstate_var = CharacterState.Idle;

        controller = GetComponent<CharacterController>();
        shooter = GetComponent<WeaponController>();

        if (shooter == null)
            Debug.Log("Missing a weapon controller");
        if (animator == null)
            Debug.Log("Missing an animator");
        if (controller == null)
            Debug.Log("Missing a character controller");
    }

    public virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        //MyBody = rigidbody;
        myGO = gameObject;
        myTransform = transform;

        default_input = myGO.GetComponent<KeyboardInput>();
        if(default_input == null)
            default_input = myGO.AddComponent<KeyboardInput>();

        myPlayerController = myGO.AddComponent<PlayerManager>();

        if (myPlayerController != null)
            myPlayerController.Init();
    }

    public void SetUserInput(bool setInput)
    {
        canControl = setInput;
    }

    public virtual void GetInput()
    {
        horz = Mathf.Clamp(default_input.GetHorizontal(), -1, 1);
        vert = Mathf.Clamp(default_input.GetVertical(), -1, 1);
    }

    public virtual void LateUpdate()
    {
        if (canControl)
            GetInput();
    }

    public bool moveDirectionally;

    private Vector3 targetDirection;
    private float curSmooth;
    private float targetSpeed;
    private float curSpeed;

    void UpdateSmoothedMovementDirection()
    {
        if(moveDirectionally)
        {
            UpdateDirectionalMovement();
        }
        else
        {
            UpdateRotationMovement();
        }
    }

    void UpdateDirectionalMovement()
    {
        targetDirection = horz * cam.GetViewedTransformRight();
        targetDirection += vert * cam.GetViewedTransformForward();

        if (targetDirection != Vector3.zero)
        {
            moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
            moveDirection = moveDirection.normalized;
        }

        curSmooth = speedSmoothing * Time.deltaTime;

        targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

        

        if((default_input.Up || default_input.Down || default_input.Right || default_input.Left) && default_input.GetRun())
        {
            targetSpeed *= runspeed;
            characterstate_var = CharacterState.Running;
        }
        else if(default_input.Up || default_input.Down || default_input.Right || default_input.Left)
        {
            targetSpeed *= walkspeed;
            characterstate_var = CharacterState.Walking;
        }
        else
        {
            characterstate_var = CharacterState.Idle;
        }

        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        controller.Move(movement);

        myTransform.rotation = Quaternion.LookRotation(moveDirection);
    }

    void UpdateRotationMovement()
    {
        myTransform.Rotate(0, horz * rotateSpeed * Time.deltaTime, 0);
        curSpeed = moveSpeed * vert;
        controller.SimpleMove(myTransform.forward * curSpeed);

        targetDirection = vert * myTransform.forward;

        float curSmooth = speedSmoothing * Time.deltaTime;

        targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

        characterstate_var = CharacterState.Idle;

        if ((default_input.Up || default_input.Down || default_input.Right || default_input.Left) && default_input.GetRun())
        {
            targetSpeed *= runspeed;
            characterstate_var = CharacterState.Running;
        }
        else if (default_input.Up || default_input.Down || default_input.Right || default_input.Left)
        {
            targetSpeed *= walkspeed;
            characterstate_var = CharacterState.Walking;
        }
        else
        {
            characterstate_var = CharacterState.Idle;
        }

        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
        
    }

    public void Update()
    {
        if(!canControl)
        {
            Input.ResetInputAxes();
        }

        UpdateSmoothedMovementDirection();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            riflepause = true;
            animator.SetInteger("state", 3);
        }
            

        //Animation
        if(!riflepause)
        {
            if (characterstate_var == CharacterState.Walking && !shooter.isHoldingWeapon())
            {
                animator.SetInteger("state", 1);
                cam.SetPlayerIdle(false);
            }
            if (characterstate_var == CharacterState.Running && !shooter.isHoldingWeapon())
            {
                animator.SetInteger("state", 2);
                cam.SetPlayerIdle(false);
            }
            if (characterstate_var == CharacterState.Idle && !shooter.isHoldingWeapon())
            {
                animator.SetInteger("state", 0);
                cam.SetPlayerIdle(true);
            }
            if (characterstate_var == CharacterState.Idle && shooter.isHoldingWeapon() && !shooter.isAiming())
            {
                animator.SetInteger("state", 3);
                shooter.CanAim(true);
                cam.SetPlayerIdle(true);
            }
            if (characterstate_var == CharacterState.Walking && shooter.isHoldingWeapon())
            {
                animator.SetInteger("state", 4);
                shooter.CanAim(true);
                cam.SetPlayerIdle(false);
            }
            if (characterstate_var == CharacterState.Running && shooter.isHoldingWeapon())
            {
                animator.SetInteger("state", 5);
                shooter.CanAim(false);
                cam.SetPlayerIdle(false);
            }

            if (characterstate_var == CharacterState.Idle && shooter.isHoldingWeapon() && shooter.isAiming())
            {
                animator.SetInteger("state", 6);
                shooter.CanAim(true);
                cam.SetPlayerIdle(true);
            }
        }
    }


    public float GetSpeed()
    {
        return moveSpeed;
    }

    public Vector3 GetDirection()
    {
        return moveDirection;
    }

    public bool isMoving()
    {
        return Mathf.Abs(vert) + Mathf.Abs(horz) > 0.5f;
    }

    public void Reset()
    {
        gameObject.tag = "Player";
    }
}
