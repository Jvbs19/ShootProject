using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] 
    float MaxLife = 15f;
    float Life;
    [SerializeField]  
    float InvensibilityFrames = 1f;
    float CanBeInvensible = 0f;
    bool HasHit = false;
    [SerializeField]  
    Image LifeBar;

    [Header("Moviment Settings")]
    [SerializeField] 
    float MoveSpeed =2.5f;
    [SerializeField]   
    float RotationSpeed =1f;
    [SerializeField]   
    float DashSpeed =5f;
    float CanDash = 0f;
    float DashCooldown = 3f;
    bool HasDash= false;

    [SerializeField] 
    Rigidbody rb;

    bool CanMove = true;
    bool CanTakeDamage = true;
    bool Dead = false;

    [SerializeField] 
    GameObject EndScreen;
    [SerializeField] 
    GameObject DashCollider;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetLife();
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            HasDash = true;
        }
        
    }
    void FixedUpdate()
    {
        if(CanMove)
        {
            Rotate();
            Move();
            if(HasDash)
            {
                if(Time.time > CanDash)
                {
                    DashCollider.SetActive(true);
                    Dash();
                    StartCoroutine(DashCol());
                    CanDash = Time.time +DashCooldown;
                }
                HasDash = false;
            }
        }
        
    }

    #region Controls
    void Move()
    {
        rb.MovePosition(rb.position + transform.forward * Input.GetAxis("Vertical") * MoveSpeed * Time.fixedDeltaTime);
    }
    void Rotate()
    {
        transform.Rotate(0, (Input.GetAxis("Horizontal") * (RotationSpeed *100) * Time.fixedDeltaTime), 0);
    }
    void Dash()
    {
        rb.MovePosition(rb.position + transform.forward * DashSpeed * Time.fixedDeltaTime);
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
            if(Time.time > CanBeInvensible)
            {
                Life -= damage;
                HealthCheck();
                CanBeInvensible = Time.time +InvensibilityFrames;
            }
            CheckForDeath();
            HasHit = false;
        }
    }
    public void SetHit(bool hit)
    {
        HasHit = hit;
    }
    void CheckForDeath()
    {
        if(Life <= 0)
        {
            CanMove = false;
            CanTakeDamage = false;
            Dead = true;
            EndScreen.SetActive(true);
        }

    }
    public bool isPlayerDead()
    {
        return Dead;
    }
    #endregion
    IEnumerator DashCol()
    {
        yield return new WaitForSeconds(0.8f);
        DashCollider.SetActive(false);
    }
}
