using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [System.Serializable]
    public class FlashSettings
    {
        public SpriteRenderer sprite;
        public bool flash;
        public int flashTimer = 10;
    }


    public bool Override;
    public FlashSettings flash;
    public GameObject player;
    public float MoveSpeed;
    public float HP;
    public bool touched;

    public int type;
    private int timer = 100;

    public void Awake()
    {
        flash.sprite = GetComponent<SpriteRenderer>();
        LevelVariables.NumOfEnemies += 1;

        if (transform.position == Vector3.zero)
        {
            transform.position = new Vector3(20, 20, 0);
        }
        else if (transform.position == new Vector3(0, 20, 0))
        {
            transform.position = new Vector3(-20, -20, 0);
        }

        if (!Override)
        {
            type = Random.Range(0, 4);
        }

        switch (type)
        {
            case 2:
                transform.localScale = new Vector2(5, 5);
                HP = 1;
                MoveSpeed = 0.13f;
                break;
            case 1:
                transform.localScale = new Vector2(6, 6);
                GetComponent<SpriteRenderer>().color = Color.yellow;
                HP = 2;
                MoveSpeed = 0.15f;
                break;
            case 0:
                transform.localScale = new Vector2(7, 7);
                GetComponent<SpriteRenderer>().color = Color.green;
                HP = 1;
                MoveSpeed = 0.16f;
                break;
            case 3:
                transform.localScale = new Vector2(10, 10);
                GetComponent<SpriteRenderer>().color = Color.red;
                HP = 4;
                MoveSpeed = 0.1f;
                break;
        }
        player = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, player.transform.position) <= 6)
        {
            transform.position = new Vector3(20, 20, 0);
        }

    }

    public void Update()
    {
        if (flash.flash)
        {
            if (flash.flashTimer > 0)
            {
                flash.flashTimer--;
                flash.sprite.enabled = false;
            }
            else
            {
                flash.sprite.enabled = true;
                flash.flash = false;
            }
        }
        

        if (player != null && !touched)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed);
        }
        else
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                touched = false;
                timer = 100;
            }
        }

        if (HP <= 0)
        {
            LevelVariables.EnemiesKilled += 1;
            LevelVariables.NumOfEnemies += -1;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDamage")
        {
            flash.flashTimer = 10;
            flash.flash = true;
            HP -= LevelVariables.AttackPower;
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.collider.tag == "Player")
        {
            touched = true;
        }
    }
}
