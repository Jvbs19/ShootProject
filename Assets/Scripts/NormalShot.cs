using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : MonoBehaviour
{
    float Damage = 1;
    float Speed = 1;
    [SerializeField]
    float DeSpawnTime = 5f;
    void Start()
    {
        Destroy(this.gameObject, DeSpawnTime);
    }
    void OnCollisionEnter(Collision col) 
    {
        if(col.transform.tag == "Player")
        {
            col.transform.GetComponent<PlayerBehaviour>().SetHit(true);
            col.transform.GetComponent<PlayerBehaviour>().DamageCheck(Damage);
            Destroy(this.gameObject);         
        }
        if(col.transform.tag == "Enemy")
        {
            col.transform.GetComponent<EnemyBehaviour>().SetHit(true);
            col.transform.GetComponent<EnemyBehaviour>().DamageCheck(Damage);
            Destroy(this.gameObject);
        }
        if(col.transform.tag == "Box")
        {
            col.transform.GetComponent<Box>().DestroyBox();
            Destroy(this.gameObject);
        }
        
    }
    void OnTriggerEnter(Collider col) 
    {
        if(col.transform.tag == "Player")
        {
            col.transform.GetComponent<PlayerBehaviour>().SetHit(true);
            col.transform.GetComponent<PlayerBehaviour>().DamageCheck(Damage);
            Destroy(this.gameObject);         
        }
        if(col.transform.tag == "Enemy")
        {
            col.transform.GetComponent<EnemyBehaviour>().SetHit(true);
            col.transform.GetComponent<EnemyBehaviour>().DamageCheck(Damage);
            Destroy(this.gameObject);
        }
        if(col.transform.tag == "Box")
        {
            col.transform.GetComponent<Box>().DestroyBox();
            Destroy(this.gameObject);
        }
        
    }
    
    #region Get/Set
    public float GetDamage()
    {
        return Damage;
    }
    public float GetSpeed()
    {
        return Speed;
    }
    public void SetDamage(float damage = 1f)
    {
        Damage = damage;
    }
    public void SetSpeed(float speed = 1f)
    {
        Speed = speed;
    }
    #endregion
}
