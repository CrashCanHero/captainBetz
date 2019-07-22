using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public GameObject shop;

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, shop.transform.position, 2f);
        Destroy(gameObject, 5);
    
    }
}
