using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] BoxSpawns;
    [SerializeField]
    GameObject  Box;
    [SerializeField]
    GameObject[] EnemySpawns;
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    PlayerBehaviour Player;
    int dice;
    int spawnPlace;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoASpawn", 10f);
    }

    void SpawnBox()
    {
        if(!Player.isPlayerDead())
        {
            dice = Random.Range(1,6);
            for (int i = 0; i < dice; i++)
            {           
            spawnPlace = Random.Range(0, BoxSpawns.Length-1);
            GameObject e = Instantiate(Box);  
            e.transform.position = BoxSpawns[spawnPlace].transform.position;
            e.transform.rotation = BoxSpawns[spawnPlace].transform.rotation;
            }
        }
    }
    void SpawnEnemy()
    {
        if(!Player.isPlayerDead())
        {
            dice = Random.Range(1,6);
            for (int i = 0; i < dice; i++)
            {           
                spawnPlace = Random.Range(0, EnemySpawns.Length-1);
                GameObject e = Instantiate(Enemy);  
                e.transform.position = EnemySpawns[spawnPlace].transform.position;
                e.transform.rotation = EnemySpawns[spawnPlace].transform.rotation;
            }
        }
    }
    void DoASpawn()
    {
        SpawnBox();
        SpawnEnemy();
        Invoke("DoASpawn", 10f);
    }
    
}
