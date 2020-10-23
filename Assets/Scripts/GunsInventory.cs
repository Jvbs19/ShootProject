using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunsInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField]
    GameObject CurrentPowerUp;
    GameObject FinalPowerUp;
    [SerializeField]
    GameObject[] PowerUps = new GameObject[4];

    [Header("Shooting Settings")]
    [SerializeField]
    GameObject normalShot;

    [SerializeField]
    float NormalShotAmmo = 5f;
    [SerializeField]
    float NormalShotDamage = 1f;
    float NormalShotSpeed = 10f;

    bool CanShoot = true;
    [SerializeField]
    float ShootCooldown = 1f;
    float CanShotBullet = 0f;
    [SerializeField]
    Transform Cannon;
    
    [Header("Collect Settings")]
    [SerializeField]
    float range = 3; 
    GameObject collected;
    bool HasCollected;
    bool HasShot;
    [Header("Canvas")]
    [SerializeField]
    Text AmmoText;

    [SerializeField]
    bool DrawGizmos = true;

    void Start()
    {
        
    }
    void Update()
    {
        Keep();
        if (Input.GetButtonDown("NormalShoot") && NormalShotAmmo>=1)
        {
            Shoot();
            
        }
        if (Input.GetButtonDown("SpecialShoot") && PowerUps[0] != null)
        {
            SpeciaShoot();
            
        }
        if (Input.GetButtonDown("ChangeLeft" ) && PowerUps[0] != null)
        {
            ChangeLeft();
        }
        if (Input.GetButtonDown("ChangeRight") && PowerUps[0] != null)
        {
            ChangeRight();
        }
        Collecting();
        AmmoText.text =""+NormalShotAmmo;
    }
    #region Shooting
    public void Shoot()
    {
        if(Time.time > CanShotBullet && CanShoot)
        {
            GameObject b = Instantiate(normalShot);  
            b.transform.position = Cannon.position;
            b.transform.rotation = Cannon.rotation;
            Rigidbody bulletRb = b.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * NormalShotSpeed;
            b.GetComponent<NormalShot>().SetDamage(NormalShotDamage);
            RemoveNormalAmmo(1);
            CanShotBullet = Time.time + ShootCooldown;
        }              
    }
    public void SpeciaShoot()
    {
        if(Time.time > CanShotBullet && CanShoot)
        {
            Remove();
            GameObject b = Instantiate(CurrentPowerUp);  
            b.transform.position = Cannon.position;
            b.transform.rotation = Cannon.rotation;
            Rigidbody bulletRb = b.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * NormalShotSpeed;
            CanShotBullet = Time.time + ShootCooldown;
        }         
    }
    #endregion

    #region Inventory
    public void ChangeLeft()
    {
        if (PowerUps[PowerUps.Length - 1] != null)
        {
            GameObject aux = PowerUps[0];
            for (int i = 0; i < PowerUps.Length - 1; i++)
            {
                PowerUps[i] = PowerUps[i + 1];
            }
            PowerUps[PowerUps.Length - 1] = aux;
        }
        else
        {
            GameObject Aux = PowerUps[0];
            int cont = 0;
            for (int i = 0; i < PowerUps.Length; i++)
            {
                if (PowerUps[i] != null)
                {
                    cont++;
                }
            }
            for (int i = 0; i < cont - 1; i++)
            {
                PowerUps[i] = PowerUps[i + 1];
            }
            PowerUps[cont - 1] = Aux;
        }

    }
    public void ChangeRight()
    {
        if (PowerUps[PowerUps.Length - 1] != null)
        {

            GameObject aux = PowerUps[PowerUps.Length - 1];
            for (int i = PowerUps.Length - 1; i > 0; i--)
            {
                PowerUps[i] = PowerUps[i - 1];
            }
            PowerUps[0] = aux;
        }
        else
        {

            int cont = 0;
            for (int i = 0; i < PowerUps.Length - 1; i++)
            {
                if (PowerUps[i] != null)
                {
                    cont++;
                }
            }
            GameObject Aux = PowerUps[cont - 1];
            for (int i = cont - 1; i > 0; i--)
            {
                PowerUps[i] = PowerUps[i - 1];
            }
            PowerUps[0] = Aux;
        }
    }

    public void Add(GameObject PowerUps)
    {
        for (int i = 0; i < this.PowerUps.Length; i++)
        {
            if (this.PowerUps[i] == null)
            {
                this.PowerUps[i] = PowerUps;
                SetCollect(true);
                i = this.PowerUps.Length - 1;
            }
        }
    }
    public void RemoveAll()
    {
        for (int i = 0; i < PowerUps.Length; i++)
        {
            PowerUps[i] = null;
        }
    }

    public void Remove()
    {
        if (PowerUps[PowerUps.Length - 1] != null)
        {
            for (int i = 0; i < PowerUps.Length - 1; i++)
            {
                PowerUps[i] = PowerUps[i + 1];
            }
            PowerUps[PowerUps.Length - 1] = null;
        }
        else
        {
            int cont = 0;
            for (int i = 0; i < PowerUps.Length; i++)
            {
                if (PowerUps[i] != null)
                {
                    cont++;
                }
            }
            for (int i = 0; i < cont - 1; i++)
            {
                PowerUps[i] = PowerUps[i + 1];
            }
            PowerUps[cont - 1] = null;
        }
    }
    void Keep()
    {

        if (PowerUps[0] != null)
        {
            CurrentPowerUp = PowerUps[0];

        }
        else
        {
            CurrentPowerUp = null;
        }
        if (PowerUps[3] != null)
        {
            FinalPowerUp = PowerUps[3];

        }
        else
        {
            FinalPowerUp = null;
        }

    }
    
    #endregion

    #region Getters/Setters
    public void SetCollect(bool a)
    {
        HasCollected = a;
    }

    public bool GetCollect()
    {
        return HasCollected;
    }
    public void AddNormalAmmo(float ammo)
    {
        NormalShotAmmo += ammo;
    }
    public void RemoveNormalAmmo(float ammo)
    {
        NormalShotAmmo -= ammo;
    }
    public bool GetCanShoot()
    {
        return CanShoot;
    }
    public void SetCanShoot(bool s)
    {
        CanShoot = s;
    }
    public GameObject[] GetInventory()
    {
        return PowerUps;
    }
    #endregion

    #region Collect
    private Collider[] CollectionArea()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, range);

        return hitColliders;
    }
    void Collecting()
    {
        Collider[] collider = CollectionArea();
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {              
                if (collider[i].gameObject.tag == "Ammo")
                {
                    if(FinalPowerUp == null)
                    {
                        Add(collider[i].gameObject.GetComponent<Ammo>().GetAmmo());
                        SetCollect(true);
                        if (GetCollect())
                        {   
                            Destroy(collider[i].gameObject);
                            SetCollect(false);
                        }
                    }
                }
                if (collider[i].gameObject.tag == "NormalAmmo")
                {
                    AddNormalAmmo(collider[i].gameObject.GetComponent<NormalAmmo>().GetAmount());
                    Destroy(collider[i].gameObject);
                }
            }
        }
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        if(DrawGizmos)
        {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
