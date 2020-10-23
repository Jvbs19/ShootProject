using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    float MaxLife = 3f;
    float Life;
    [SerializeField]
    Image LifeBar;
    bool HasHit = false;
    bool Dead = false;

    [Header("Enemy Settings")]
    [SerializeField]
    float EnemySpeed = 1f;
    [SerializeField]
    float RotationSpeed =2;

    float RotateTime;
    [SerializeField]
    float MinRotateTime = 2;
    [SerializeField]
    float MaxRotateTime = 10;
    float offset = 1;
    bool CanMove = false;
    bool NoMoreMoving = false;
    bool NoMoreShooting = false;
    bool CanShoot = true;
    bool CanLook = true;
    bool CanTakeDamage = true;

    [SerializeField]
    float HitDamage = 1f;
    Rigidbody rb;

    [Header("Player Settings")]
    [SerializeField]
    GameObject PlayerLocation;
    [SerializeField]
    float SafeDistance = 3f;
    [SerializeField]
    float SearchRange = 10f;

    [Header("Bullet Settings")]
    [SerializeField]
    float BulletDamage = 1f;
    [SerializeField]
    float BulletSpeed = 3f;
    [SerializeField]
    float BulletCooldown = 3f;
    float CanShotBullet = 0f;
    [SerializeField]
    Transform Cannon;
    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    bool DrawGizmos = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetLife();
    }
    void Update()
    {
        if(PlayerLocation == null)
        {
            FindPlayer();
        }  
        else
        {
            transform.LookAt(PlayerLocation.transform, Vector3.forward);
            ChasePlayer();
            CheckPlayerDeath();
        }
        CheckDeath();       
    }
    void FixedUpdate()
    {
        MoveEnemy();
        if(PlayerLocation == null)
        {
            RotateEnemy();
        }
    }
    #region Behaviours
    void FindPlayer()
    {
        CanMove = true;
        Collider[] collider = CollisionArea();
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].gameObject.tag != this.gameObject.tag)
                {
                    if (collider[i].gameObject.tag == "Player")
                    {
                        PlayerLocation = collider[i].gameObject;
                    }
                }
            }
        }
    }
    private Collider[] CollisionArea()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, SearchRange);

        return hitColliders;
    }
    void MoveEnemy()
    {
        if(CanMove && !Dead && !NoMoreMoving)       
             rb.MovePosition(rb.position + transform.forward * EnemySpeed  * Time.fixedDeltaTime);
    }
    void RotateEnemy()
    {
        if(RotateTime > 0)
        {
            RotateTime -= Time.deltaTime;
        }
        else
        {
            RotateTime = Random.Range(MinRotateTime, MaxRotateTime);
            
            if(CanMove == true && !Dead)
                transform.eulerAngles = new Vector3(0,Random.Range(1,360),0);
        }    
    }

    void ChasePlayer()
    {
        if(!Dead)
        {
            if(Vector3.Distance(this.transform.position, PlayerLocation.transform.position) >= SafeDistance)
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
                ShootPlayer();
            }
        }
        else
        {
            CanMove = true;
        }
    }
    void ShootPlayer()
    {
        
        if(Time.time > CanShotBullet && CanShoot && !NoMoreShooting)
        {
            GameObject b = Instantiate(Bullet);  
            b.transform.position = Cannon.position;
            b.transform.rotation = Cannon.rotation;
            Rigidbody bulletRb = b.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * BulletSpeed;
            b.GetComponent<NormalShot>().SetDamage(BulletDamage);
            CanShotBullet = Time.time + BulletCooldown;
        }                       
    }
    #endregion

    #region Status
    void ResetLife()
    {
        Life = MaxLife;
    }
    void HealthCheck()
    {
        if(LifeBar!= null)
        LifeBar.fillAmount = Life / MaxLife;
    }
    public void DamageCheck(float damage)
    {
        if(HasHit && CanTakeDamage)
        {        
            Life -= damage;
            HealthCheck();
            HasHit = false;
        }
    }
    public void SetHit(bool hit)
    {
        HasHit = hit;
    }
    void CheckDeath()
    {
        if(Life <= 0 && !Dead)
        {
            Life = 0;
            CanMove = false;
            CanShoot = false;
            CanLook = false;
            CanTakeDamage = false;
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().AddScore(20);
            Dead = true;
            Destroy(this.gameObject);
        }

    }
    void CheckPlayerDeath()
    {
        if(PlayerLocation.GetComponent<PlayerBehaviour>().isPlayerDead())
        {
            Dead = true;
            CanMove = false;
            CanShoot = false;
            CanLook = false;
            CanTakeDamage = false;
        }
    }
    #endregion

    #region Effects
    public void HalfSpeed()
    {
        EnemySpeed = EnemySpeed / 2;
    }

    public void StopMoving()
    {
        NoMoreMoving = true;
    }
    public void StopShooting()
    {
        NoMoreShooting = true;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        if(DrawGizmos)
        {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SearchRange);
        }
    }

}
