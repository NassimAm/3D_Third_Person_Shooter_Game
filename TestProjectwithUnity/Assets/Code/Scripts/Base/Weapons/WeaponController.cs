using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weapons;
    public Camera cam;
    public GameObject impactEffect;
    public float aimingFieldofView = 30f;
    public float aimingSpeed = 1f;
    private float initialFieldofView;

    private int selectedWeaponSlot;
    private int lastSelectedWeaponSlot;

    //public Vector3 offsetWeaponSpawnPosition;

    public Transform forceParent;

    private ArrayList weaponSlots;
    private ArrayList weaponScripts;
    private WeaponScript tempWeapon;
    private Vector3 tempVec;
    private Quaternion tempRotation;
    private GameObject tempGO;

    private Transform myTransform;
    private int ownerNum;

    public bool useForceVectorDirection;
    public Vector3 forceVector;
    private Vector3 theDir;

    private KeyboardInput default_input;
    private RaycastHit hit;
    private DestructableObject tempDestructable;
    private Enemy tempEnemy;
    private bool holdWeapon = false;
    private bool aiming = false;
    private bool canAim = false;

    public void Start()
    {
        initialFieldofView = cam.fieldOfView;
        
        selectedWeaponSlot = 0;
        lastSelectedWeaponSlot = -1;

        weaponSlots = new ArrayList();
        weaponScripts = new ArrayList();

        myTransform = transform;

        if(forceParent == null)
        {
            forceParent = myTransform;
        }

        tempVec = forceParent.position;
        tempRotation = forceParent.rotation;

        for(int i=0;i<weapons.Length;i++)
        {
            tempGO = (GameObject)Instantiate(weapons[i], tempVec, tempRotation);

            tempWeapon = tempGO.GetComponent<WeaponScript>();
            weaponScripts.Add(tempWeapon);

            tempGO.transform.parent = forceParent;
            tempGO.layer = forceParent.gameObject.layer;
            tempGO.transform.localPosition = tempWeapon.spawnPosOffset;
            tempGO.transform.localRotation = Quaternion.Euler(tempWeapon.spawnRotOffset);

            weaponSlots.Add(tempGO);

            tempGO.SetActive(false);
        }

        default_input = gameObject.GetComponent<KeyboardInput>();
        if (default_input == null)
            default_input = gameObject.AddComponent<KeyboardInput>();

        SetWeaponSlot(0);
    }

    public void Update()
    {
        //Slot1-------------------------------------------------
        if (default_input.slot1)
        {
            holdWeapon = false;
            SetWeaponSlot(0);
            DisableAllWeapons();
        }
        //Slot2----------------------------------------------------------
        if (default_input.slot2)
        {
            SetWeaponSlot(1);
            holdWeapon = true;
        }
        //Slot3--------------------------------------------------------------
        if (default_input.slot3)
        {
            SetWeaponSlot(2);
            holdWeapon = true;
        }

        if (holdWeapon && default_input.Fire2 && canAim)
        {
            aiming = true;
            if (cam.fieldOfView <= aimingFieldofView)
            {
                cam.fieldOfView = aimingFieldofView;
            }
            else
            {
                cam.fieldOfView -= aimingSpeed;
            }
        }

        if (!default_input.Fire2 || !canAim)
        {
            aiming = false;
            if (cam.fieldOfView >= initialFieldofView)
            {
                cam.fieldOfView = initialFieldofView;
            }
            else
            {
                cam.fieldOfView += aimingSpeed;
            }
        }

        if (default_input.Fire1 && holdWeapon)
            Fire();

    }

    

    public void SetOwner(int aNum)
    {
        ownerNum = aNum;
    }

    public void SetWeaponSlot(int slotNum)
    {
        if(slotNum == 0)
        {
            holdWeapon = false;
        }
        else
        {
            if(slotNum - 1 == lastSelectedWeaponSlot)
            {
                return;
            }

            DisableCurrentWeapon();

            selectedWeaponSlot = slotNum - 1;

        
            if (selectedWeaponSlot < 0)
                selectedWeaponSlot = weaponSlots.Count - 1;

            if (selectedWeaponSlot > weaponSlots.Count - 1)
                selectedWeaponSlot = 0;

            lastSelectedWeaponSlot = selectedWeaponSlot;

            EnableCurrentWeapon();
        } 
    }

    public void NextWeaponSlot()
    {
        DisableCurrentWeapon();

        selectedWeaponSlot++;

        if(selectedWeaponSlot == weaponSlots.Count)
        {
            selectedWeaponSlot = 0;
        }

        lastSelectedWeaponSlot = selectedWeaponSlot;

        EnableCurrentWeapon();
    }

    public void PrevWeaponSlot()
    {
        DisableCurrentWeapon();

        selectedWeaponSlot--;

        if (selectedWeaponSlot < 0)
        {
            selectedWeaponSlot = weaponSlots.Count -1;
        }

        lastSelectedWeaponSlot = selectedWeaponSlot;

        EnableCurrentWeapon();
    }

    public void DisableCurrentWeapon()
    {
        if (weaponScripts.Count == 0)
            return;

        tempWeapon = (WeaponScript)weaponScripts[selectedWeaponSlot];
        tempWeapon.Disable();

        tempGO = (GameObject)weaponSlots[selectedWeaponSlot];
        tempGO.SetActive(false);
    }

    public void EnableCurrentWeapon()
    {
        if (weaponScripts.Count == 0)
            return;

        tempWeapon = (WeaponScript)weaponScripts[selectedWeaponSlot];
        tempWeapon.Enable();

        tempGO = (GameObject)weaponSlots[selectedWeaponSlot];
        tempGO.SetActive(true);
    }

    public void DisableAllWeapons()
    {
        if (weaponScripts.Count == 0)
            return;

        for(int i= 0;i<weaponScripts.Count;i++)
        {
            tempWeapon = (WeaponScript)weaponScripts[i];
            tempWeapon.Disable();

            tempGO = (GameObject)weaponSlots[i];
            tempGO.SetActive(false);
        }

        lastSelectedWeaponSlot = -1;
    }

    public void Fire()
    {
        if (weaponScripts == null)
            return;

        if (weaponScripts.Count == 0)
            return;

        tempWeapon = (WeaponScript)weaponScripts[selectedWeaponSlot];

        theDir = cam.transform.forward;

        if (useForceVectorDirection)
            theDir = forceVector;


        tempWeapon.Fire(theDir, ownerNum);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && tempWeapon.isLoaded)
        {
            tempGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            tempDestructable = hit.collider.GetComponent<DestructableObject>();
            tempEnemy = hit.collider.GetComponentInParent<Enemy>();

            if (tempDestructable != null)
            {
                if (tempDestructable.health >= 0.0f)
                    tempDestructable.ReduceHealth(tempWeapon.damage);
                
                if (hit.rigidbody == null)
                    Debug.Log("Missing a rigidbody");
                else
                    hit.rigidbody.AddForce(-hit.normal * tempWeapon.knockback);
            }
            else if(tempEnemy != null)
            {
                tempEnemy.RagDoll(true);
                hit.rigidbody.AddForce(-hit.normal * tempWeapon.knockback);
            }

            Destroy(tempGO, 1.0f);
        }

    }

    public bool isHoldingWeapon()
    {
        return holdWeapon;
    }

    public bool isAiming()
    {
        return aiming;
    }

    public void CanAim(bool value)
    {
        canAim = value;
    }

    
}