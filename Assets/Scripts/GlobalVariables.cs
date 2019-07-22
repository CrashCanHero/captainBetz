using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static string Facing;
    public static int Score;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
