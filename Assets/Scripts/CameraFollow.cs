using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float offset = 20f;

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(player.position.x, player.position.y +offset, player.position.z), 3f);
    }
}
