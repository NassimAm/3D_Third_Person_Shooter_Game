using UnityEngine;

public class CamMovement : MonoBehaviour
{
    private Vector2 mousePos;
    private float xRotation;
    private float yRotation;
    private Vector3 camRight;
    private bool playerIsIdle;

    public Transform player;
    private Transform camTransform;
    public WeaponController shooter;
    public Transform playerspine;
    public float spineOffsetRot = 20.0f;

    public float camSensibility = 5.0f;
    public Vector3 height = new Vector3(0.0f,2.5f,0.0f);
    public float FPSlookoffset = 0.3f;

    private bool FPSlook = true;
    private bool f5released = true;
    private bool doneswitchinglooks = false;

    public Vector3 camOffsetThirdPerson = new Vector3(0.0f, 0.0f , -3.0f);
    public Vector3 camRadiusThirdPerson = new Vector3(3.0f, 2.0f, 3.0f);
    public float shooterOffset = 0.75f;
    // Start is called before the first frame update
    void Awake()
    {
        camTransform = GetComponent<Transform>();

        if (player == null)
            Debug.Log("Player missing");
        if (playerspine == null)
            Debug.Log("Missing a player spine");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mousePos.x = Input.GetAxis("Mouse X") * camSensibility;
        mousePos.y = Input.GetAxis("Mouse Y") * camSensibility;

        xRotation -= mousePos.y;
        yRotation += mousePos.x;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        camRight = new Vector3(Mathf.Cos(-yRotation * Mathf.Deg2Rad), 0.0f, Mathf.Sin(-yRotation * Mathf.Deg2Rad));


        //Checking FPS State-----------------------------------------------------------
        if (Input.GetKey(KeyCode.F5) && f5released)
        {
            if (!doneswitchinglooks && FPSlook)
            {
                camTransform.position = player.position + camOffsetThirdPerson;
                FPSlook = false;
                doneswitchinglooks = true;
            }
            if (!doneswitchinglooks && !FPSlook)
            {
                FPSlook = true;
                doneswitchinglooks = true;
            }

            f5released = false;
        }
        if (Input.GetKeyUp(KeyCode.F5))
        {
            f5released = true;
            doneswitchinglooks = false;
        }

        //FPS Scripts------------------------------------------------------------------
        if (FPSlook)
        {
            camTransform.position = player.position + height + FPSlookoffset * player.forward;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
            if(playerIsIdle)
                player.Rotate(Vector3.up * yRotation);
        }
        else
        {
            camOffsetThirdPerson = new Vector3(camRadiusThirdPerson.x * Mathf.Sin(-yRotation * Mathf.Deg2Rad) * Mathf.Cos(xRotation * Mathf.Deg2Rad), camRadiusThirdPerson.y * Mathf.Sin(xRotation * Mathf.Deg2Rad), camRadiusThirdPerson.z * Mathf.Cos(xRotation * Mathf.Deg2Rad) * (-Mathf.Cos(-yRotation * Mathf.Deg2Rad)));
            if(camOffsetThirdPerson.x < 0.01f && camOffsetThirdPerson.x > -0.01f && camOffsetThirdPerson.z < 0.01f && camOffsetThirdPerson.z > -0.01f)
            {
                camOffsetThirdPerson.x = 0.05f * Mathf.Sin(-yRotation * Mathf.Deg2Rad);
                camOffsetThirdPerson.z = 0.05f * (-Mathf.Cos(-yRotation * Mathf.Deg2Rad));
            }
            camTransform.position = player.position + height + camOffsetThirdPerson + shooterOffset * camRight;
            //camTransform.position = new Vector3(camTransform.position.x, Mathf.Clamp(camTransform.position.y, player.position.y + height.y - camRadiusThirdPerson.y + 1f, player.position.y + height.y + camRadiusThirdPerson.y - 1f), camTransform.position.z);
            camTransform.LookAt(player.position + height + shooterOffset * camRight);

            if(shooter.isHoldingWeapon())
            {
                if(playerIsIdle)
                    player.eulerAngles = new Vector3(0.0f,yRotation,0.0f);

                playerspine.Rotate(new Vector3(Mathf.Cos(Vector3.Angle(player.right,playerspine.right)*Mathf.Deg2Rad) * player.right.magnitude,0.0f, Mathf.Sin(Vector3.Angle(player.right, playerspine.right) * Mathf.Deg2Rad) * player.right.magnitude),xRotation);
            }
        }

    }

    public Vector3 GetViewedTransformForward()
    {
        return new Vector3(Mathf.Sin(yRotation * Mathf.Deg2Rad),0.0f,Mathf.Cos(yRotation * Mathf.Deg2Rad));
    }
    public Vector3 GetViewedTransformRight()
    {
        return new Vector3(Mathf.Cos(yRotation * Mathf.Deg2Rad), 0.0f, -Mathf.Sin(yRotation * Mathf.Deg2Rad));
    }

    public bool getFPSlook()
    {
        return FPSlook;
    }

    public Vector3 getHeight()
    {
        return height;
    }

    public Vector3 getCamThirdPersonOffset()
    {
        return camOffsetThirdPerson;
    }

    public void SetPosition(Vector3 aposition)
    {
        camTransform.position = aposition;
    }

    public float GetFPSlookOffset()
    {
        return FPSlookoffset;
    }

    public void SetPlayerIdle(bool value)
    {
        playerIsIdle = value;
    }
}
