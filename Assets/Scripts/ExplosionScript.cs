using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject player;

    public void Update()
    {
        //if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
        //{
        //    if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
        //    {
        //        transform.localPosition = new Vector2(0, 0.4f);
        //        transform.rotation = Quaternion.Euler(0, 0, 90);
        //    }
        //    else if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        //    {
        //        transform.localPosition = new Vector2(0, -0.4f);
        //        transform.rotation = Quaternion.Euler(0, 0, 90);
        //    }
        //    else
        //    {
        //        transform.localPosition = new Vector2(0.4f, 0);
        //        transform.rotation = Quaternion.Euler(0, 0, 0);
        //    }
        //}
        //else if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
        //{
        //    if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
        //    {
        //        transform.localPosition = new Vector2(0, 0.4f);
        //        transform.rotation = Quaternion.Euler(0, 0, 90);
        //    }
        //    else if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        //    {
        //        transform.localPosition = new Vector2(0, -0.4f);
        //        transform.rotation = Quaternion.Euler(0, 0, 90);
        //    }
        //    else
        //    {
        //        transform.localPosition = new Vector2(-0.4f, 0);
        //        transform.rotation = Quaternion.Euler(0, 0, 0);
        //    }
        //}
        //else if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
        //{
        //    transform.localPosition = new Vector2(0, 0.4f);
        //    transform.rotation = Quaternion.Euler(0, 0, 90);
        //}
        //else if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        //{
        //    transform.localPosition = new Vector2(0, -0.4f);
        //    transform.rotation = Quaternion.Euler(0, 0, 90);
        //}

        switch (CharacterScript.facing)
        {
            case 0:
                transform.localPosition = new Vector2(0, 0.4f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 1:
                transform.localPosition = new Vector2(0.4f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                transform.localPosition = new Vector2(0, -0.4f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                transform.localPosition = new Vector2(-0.4f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}
