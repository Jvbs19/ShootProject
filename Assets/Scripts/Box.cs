using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    GameObject NormalAmmo;
    [SerializeField]
    GameObject SpecialAmmo;
    int dice, TimeA, TimeSA;

    public void DestroyBox()
    {
        dice = Random.Range(1,6);
        TimeA= Random.Range(1,5);
        TimeSA = Random.Range(1,3);
        if(dice%2 == 0)
        {
            for(int i = 0; i < TimeA; i++)
            {
                GameObject a = Instantiate(NormalAmmo);  
                a.transform.position = this.transform.position;
                a.transform.rotation = this.transform.rotation;
            }

        }
        if(dice%2 ==1)
        {
            for(int i = 0; i < TimeA; i++)
            {
                GameObject a = Instantiate(NormalAmmo);  
                a.transform.position = this.transform.position;
                a.transform.rotation = this.transform.rotation;
            }
            for(int i = 0; i < TimeSA; i++)
            {
                GameObject b = Instantiate(SpecialAmmo);  
                b.transform.position = this.transform.position;
                b.transform.rotation = this.transform.rotation;
            }
        }
        GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreController>().AddScore(10);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "DashCollider")
        {
            DestroyBox();
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "DashCollider")
        {
            DestroyBox();
        }
    }
}
