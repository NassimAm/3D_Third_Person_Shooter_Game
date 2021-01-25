using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private 

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        RagDoll(false);
    }

    public void RagDoll(bool value)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !value;
        }
    }
}
