using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [System.NonSerialized]
    public bool canFire;

    public float damage = 2.0f;
    public float knockback = 1.0f;
    public int ammo = 100;
    public int maxAmmo = 100;

    public bool isInfiniteAmmo;
    public float fireRate = 50.0f;

    private Vector3 fireVector;

    [System.NonSerialized]
    public Transform myTransform;

    private int myLayer;

    public Vector3 spawnPosOffset;
    public Vector3 spawnRotOffset;
    public float forwardOffset = 1.5f;
    public float projectileSpeed = 10.0f;
    public bool inheritVelocity;

    public bool isLoaded;

    private float lasttime = 0.0f;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        myTransform = transform;

        myLayer = gameObject.layer;

        isLoaded = true;
    }

    public void Enable()
    {
        if (canFire == true)
            return;

        canFire = true;
    }

    public void Disable()
    {
        if (canFire == false)
            return;

        canFire = false;
    }

    public void Fire(Vector3 aDirection, int ownerID)
    {
        if (!canFire)
            return;

        //if (ammo <= 0 && !isInfiniteAmmo)

        if(isLoaded)
            ammo--;


        if (Time.time > lasttime + 1.0f/fireRate)
        {
            isLoaded = true;
            lasttime = Time.time;
        }
        else
        {
            isLoaded = false;
        }
            
    }
}
