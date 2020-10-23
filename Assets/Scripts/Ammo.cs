using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    GameObject AmmoType;

    [SerializeField]
    GameObject[] DATABASE;

    void Start() 
    {
        int a = Random.Range(0,DATABASE.Length-1);
        AmmoType = DATABASE[a];
        
    }

    public GameObject GetAmmo()
    {
        return AmmoType;
    }
}

