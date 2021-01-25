using UnityEngine;

public class ExtendedCustomMonoBehavior : MonoBehaviour
{
    public Transform myTransform;
    public GameObject myGO;
    public Rigidbody myBody;

    public bool didInit;
    public bool canControl;

    public int id;

    public Vector3 tempVEC;
    public Transform tempTR;

    public virtual void setID(int anID)
    {
        id = anID;
    }
}
