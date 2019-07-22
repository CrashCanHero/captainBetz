using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class UIScript : MonoBehaviour
{
    public bool Interactable, isExit;
    public SpriteRenderer render;
    public Sprite sprite1, sprite2;

    public UnityEvent onClick;

    public void Start()
    {
        render = GetComponent<SpriteRenderer>();
        if (onClick == null)
        {
            onClick = new UnityEvent();
        }
    }

    public void OnMouseDown()
    {
        if (!isExit)
        {
            render.sprite = sprite2;
        }
    }

    public void OnMouseUp()
    {
        if (!isExit)
        {
            render.sprite = sprite1;
        }

        if (Interactable)
        {
            onClick.Invoke();
        }
    }
}
