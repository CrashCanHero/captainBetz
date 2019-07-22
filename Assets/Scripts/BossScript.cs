using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip attack, scream;

    public Camera cam;
    private float Dist;

    public bool touched;
    public int waitTimer = 40;

    public string Phase;
    public int timer;
    private int time = 500;

    public Image white;
    public TMP_Text text;
    public Sprite normal, attacking, hurt;

    public GameObject Player;

    private Vector3 OriginalPos;
    private bool talk, talked = false;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
        OriginalPos = transform.position;
        timer = Random.Range(40, 200);
        text.enabled = false;
        white.enabled = false;
    }

    public void Update()
    {
        if (waitTimer > 0)
        {
            touched = true;
            waitTimer--;
        }
        else
        {
            touched = false;
        }

        if (BossStats.Health > 0)
        {
            if (timer < 0)
            {
                talk = true;
                SlashPhase();
            }
            else
            {
                timer--;
                WaitPhase();
            }
        }
        else
        {
            if (timer > 0)
            {
                timer--;
                white.color = Color.white;
                white.enabled = true;
                text.enabled = true;
            }
            else if (timer <= 0)
            {
                SceneManager.LoadScene(3);
            }
            WaitPhase();
        }
    }

    public void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.tag == "EnemyDamage")
        {
            talked = false;
            audio.PlayOneShot(scream, 0.2f);
            timer = Random.Range(40, 200);
            GetComponent<SpriteRenderer>().sprite = hurt;

            BossStats.Health -= LevelVariables.AttackPower;

            if (BossStats.Health <= 0)
            {
                timer = 2;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.collider.tag == "Player")
        {
            waitTimer = 40;
            touched = true;
        }
    }

    public void WaitPhase()
    {
        Phase = "Wait";
        int MoveArea = Random.Range(-2, 2);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(OriginalPos.x + MoveArea, 5), 0.5f);
    }

    public void SlashPhase()
    {
        Phase = "Slash";

        if (talk)
        {
            if (!talked)
            {
                audio.PlayOneShot(attack, 0.2f);
                talk = false;
                talked = true;
            }
        }

        if (Player != null)
        {
            Dist = Vector3.Distance(transform.position, Player.transform.position);
        }

        if (Dist <= 4.5f)
        {
            if (!touched)
            {
                if (Player != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.2f);
                }
                GetComponent<SpriteRenderer>().sprite = attacking;
            }
        }
        else
        {
            if (!touched)
            {
                GetComponent<SpriteRenderer>().sprite = normal;
                if (Player != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.1f);
                }
            }
        }

    }
}