using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconController : MonoBehaviour
{
    [SerializeField]
    GunsInventory inventory;
    [SerializeField]
    GameObject[] Powerups;
    [SerializeField]
    Image[] Icons;
    [SerializeField]
    Sprite Null, Push, Slow, Stop, Disable;
    void Update()
    {
        Powerups = inventory.GetInventory();
        SetIcon(0);
        SetIcon(1);
        SetIcon(2);
        SetIcon(3);
    }
    void SetIcon(int i)
    {
        if(Powerups[i]!= null)
        {
            if(Powerups[i].GetComponent<ShotBehaviour>().GetmyType() == "Push")
            {
                Icons[i].sprite = Push;
            }
            if(Powerups[i].GetComponent<ShotBehaviour>().GetmyType() == "Slow")
            {
                Icons[i].sprite = Slow;
            }
            if(Powerups[i].GetComponent<ShotBehaviour>().GetmyType() == "Stop")
            {
                Icons[i].sprite = Stop;
            }     
            if(Powerups[i].GetComponent<ShotBehaviour>().GetmyType() == "Disable")
            {
                Icons[i].sprite = Disable;
            }           
        }
        else
        {
            Icons[i].sprite = Null;
        }
    }
}
