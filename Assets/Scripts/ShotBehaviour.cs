using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField]
    Shot Behaviour;
    string type1, type2;
    GameObject Target;
    float Damage=0;
    float KnockBack=30;
    bool Twice = false;

    void Start()
    {
        SetBehaviour();
    }
    public void Use(GameObject target)
    {
        Target = target;
        if(type1 != type2)
        {
            if(type1 == "Push")
            {
                Push();
            }
            if(type1 == "Slow")
            {
                Slow();
            }
            if(type1 == "Stop")
            {
                Stop();
            }
            if(type1 == "Disable")
            {
                Disable();
            }
            if(type2 == "Push")
            {
                Push();
            }
            if(type2 == "Slow")
            {
                Slow();
            }
            if(type2 == "Stop")
            {
                Stop();
            }
            if(type2 == "Disable")
            {
                Disable();
            }
        }
        else
        {
            if(type1 == "Push")
            {
                Push();
            }
            if(type1 == "Slow")
            {
                Slow();
            }
            if(type1 == "Stop")
            {
                Stop();
            }
            if(type1 == "Disable")
            {
                Disable();
            }
        }
    }

    void SetBehaviour()
    {
        type1 = Behaviour.GetMyType();
        type2 = Behaviour.GetType2();
        Damage = Behaviour.GetDamage();
    }
    #region Behaviours
    void Push()
    {
        Rigidbody EnemyRb = Target.transform.gameObject.GetComponent<Rigidbody>();

        if(EnemyRb != null)
        {
            EnemyRb.isKinematic = false;
            Vector3 Diff = EnemyRb.transform.position - transform.position;
            Diff = Diff.normalized * KnockBack;
            EnemyRb.AddForce(Diff, ForceMode.Impulse);
            StartCoroutine(KnockbackImp(EnemyRb, 1f));
        }
    }

    void Slow()
    {
        Target.transform.GetComponent<EnemyBehaviour>().HalfSpeed();
    }
    void Stop()
    {
        Target.transform.GetComponent<EnemyBehaviour>().StopMoving();
    }
    void Disable()
    {
        Target.transform.GetComponent<EnemyBehaviour>().StopShooting();
    }
    #endregion
    private IEnumerator KnockbackImp(Rigidbody Enemy, float KnockTime)
    {
        if (Enemy != null)
        {
            yield return new WaitForSeconds(KnockTime);
            Enemy.velocity = Vector3.zero;
            Enemy.isKinematic = true;
        }
    }
    public string GetmyType()
    {
        return Behaviour.GetMyType();
    }
}
