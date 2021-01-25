using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;

    public bool Fire1;
    public bool Fire2;
    public bool Run;

    public bool slot1;
    public bool slot2;
    public bool slot3;
    public bool slot4;
    public bool slot5;
    public bool slot6;
    public bool slot7;
    public bool slot8;
    public bool slot9;

    public float vert;
    public float horz;
    public bool shouldRespawn;

    private Vector3 tempVec3;

    public virtual void CheckInput()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    public virtual float GetHorizontal()
    {
        return horz;
    }

    public virtual float GetVertical()
    {
        return vert;
    }

    public virtual bool GetFire()
    {
        return Fire1;
    }

    public virtual bool GetRun()
    {
        return Run;
    }

    public bool GetRespawn()
    {
        return shouldRespawn;
    }

    public virtual Vector3 GetMovementDirectionVec()
    {
        tempVec3 = Vector3.zero;

        if(Left||Right)
        {
            tempVec3.x = horz;
        }
        if(Up||Down)
        {
            tempVec3.y = vert;
        }

        return tempVec3;
    }
}
