using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public float health = 200.0f;
    public GameObject destroyed_obj;
    public void ReduceHealth(float damage)
    {
        health -= damage;
    }

    void Update()
    {
        if(health < 0.0f)
        {
            health = 0.0f;
            Instantiate(destroyed_obj, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
