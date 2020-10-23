using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAmmo : MonoBehaviour
{
    float ammoAmout;
    void Start()
    {
        ammoAmout = Random.Range(1,5);
    }
    public float GetAmount()
    {
        return ammoAmout;
    }
}
