using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPhysics : MonoBehaviour
{
    [SerializeField]
    float CastSize =2f;
    [SerializeField]
    float BulletSpeed =1;
    GameObject Target;
    [SerializeField]
    bool DrawGizmos = true;

    void Update()
    {
        Findplayer();
    }
    private Collider[] Area()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, CastSize);

        return hitColliders;
    }
    void Findplayer()
    {
        Collider[] collider = Area();
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {              
                if(collider[i].gameObject.tag == "Enemy")
                {
                    Target = collider[i].gameObject;
                    GetComponent<ShotBehaviour>().Use(Target);
                    Destroy(this.gameObject);
                }
                if(collider[i].gameObject.tag == "Box")
                {
                    collider[i].transform.GetComponent<Box>().DestroyBox();
                    Destroy(this.gameObject);
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if(DrawGizmos)
        {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CastSize);
        }
    }

}
