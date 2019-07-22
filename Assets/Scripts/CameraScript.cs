using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
