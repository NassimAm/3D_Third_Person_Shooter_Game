using UnityEngine;

public class SpaceShipMovement : ExtendedCustomMonoBehavior
{
    private Quaternion targetRotation;

    private float thePos;
    private float moveXAmount;
    private float moveZAmount;

    public float moveXSpeed = 40f;
    public float moveZSpeed = 15f;

    public float limitX = 15f;
    public float limitZ = 15f;

    private float originZ;

    public KeyboardInput default_input;

    public float horizontal_input;
    public float vertical_input;

    //Transform actualTransform;

    public virtual void Start()
    {
        didInit = false;

        this.Init();
    }

    public virtual void Init()
    {
        myTransform = transform;
        myGO = gameObject;

        default_input = myGO.AddComponent<KeyboardInput>();
        //actualTransform = myGO.GetComponent<Transform>();
        canControl = true;

        originZ = myTransform.localPosition.z;

        didInit = true;
    }

    public virtual void GetInput()
    {
        horizontal_input = default_input.GetHorizontal();
        vertical_input = default_input.GetVertical();
    }

    public virtual void Update()
    {
        UpdateShip();
        //actualTransform = myTransform;
    }

    public virtual void UpdateShip()
    {
        if (!didInit)
            return;

        if (!canControl)
            return;

        GetInput();

        moveXAmount = horizontal_input * Time.deltaTime * moveXSpeed;
        moveZAmount = vertical_input * Time.deltaTime * moveZSpeed;

        Vector3 tempRotation = myTransform.eulerAngles;
        tempRotation.z = horizontal_input * -30f;
        myTransform.eulerAngles = tempRotation;

        myTransform.localPosition += new Vector3(moveXAmount, 0, moveZAmount);

        if(myTransform.localPosition.x<= -limitX || myTransform.localPosition.x>=limitX)
        {
            thePos = Mathf.Clamp(myTransform.localPosition.x, -limitX, limitX);
            myTransform.localPosition = new Vector3(thePos, myTransform.localPosition.y, myTransform.localPosition.z);
        }

        if (myTransform.localPosition.z <= -limitZ || myTransform.localPosition.z >= limitZ)
        {
            thePos = Mathf.Clamp(myTransform.localPosition.z, -limitZ, limitZ);
            myTransform.localPosition = new Vector3(myTransform.localPosition.x, myTransform.localPosition.y, thePos);
        }
    }
}
