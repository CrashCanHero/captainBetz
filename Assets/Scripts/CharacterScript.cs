using UnityEngine;
using TMPro;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class CharacterScript : MonoBehaviour
{
    [System.Serializable]
    public class AudioSettings
    {
        public AudioSource sound;
        public AudioClip ready, shoot, hurt, run;
    }

    public AudioSettings audio;

    public static int HP = 100;
    public int MaxHP = 100;
    public static int timer = 40;
    private int cooldown;
    int balltime;
    int HitTimer;
    int flashtimer;
    public float UpDown, LeftRight;

    bool move;
    public static int facing;

    public bool inBoss, attacking;

    public TMP_Text gameOver, Hp, congrat, round;

    public Button fight, attack, speed, defense;

    public Rigidbody2D rb;
    public Animator anim;
    public GameObject Explosion, E, ShopMenu, Ball;

    public void Awake()
    {
        audio.sound = GetComponent<AudioSource>();
        Explosion.GetComponent<SpriteRenderer>().enabled = false;
        Explosion.GetComponent<BoxCollider2D>().enabled = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameOver.enabled = false;
        E.SetActive(false);
        if (!inBoss)
        {
            congrat.enabled = false;
            fight.enabled = false;
            ShopMenu.SetActive(false);
        }
    }

    public void Update()
    {
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        Hp.text = "HP: " + HP + "/" + MaxHP;
        if (round != null)
        {
            round.text = "Round " + LevelVariables.Round.ToString();
        }

        Death();

        if (LevelVariables.NumOfEnemies <= 0)
        {
            if (balltime <= 0)
            {
                if (Ball != null)
                {
                    Instantiate(Ball, transform.position, Quaternion.identity);
                    balltime = 30;
                }
            }
            else
            {
                balltime--;
            }
            if (timer > 0)
            {
                timer--;
                if (congrat != null)
                {
                    congrat.enabled = true;
                }
            }
            else
            {
                if (congrat != null)
                {
                    congrat.enabled = false;
                }
            }
        }

        if (cooldown > 0)
        {
            cooldown--;
        }

        if (HitTimer > 0)
        {
            HitTimer--;
            if (flashtimer <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
                flashtimer = 10;
            }
            else
            {
                flashtimer--;
            }

        }
        else
        {
            if (GetComponent<SpriteRenderer>().enabled == false)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        move = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        UpDown = Input.GetAxis("Vertical");
        LeftRight = Input.GetAxis("Horizontal");

        if (!attacking)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                facing = 0;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                facing = 2;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                facing = 3;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                facing = 1;
            }

            if (rb.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (rb.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (move)
            {
                anim.Play("PlayerRun_anim", 0);
            }
            else
            {
                anim.Play("Player_anim", 0);
            }
            rb.velocity = new Vector2(LeftRight * LevelVariables.Speed, UpDown * LevelVariables.Speed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (cooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (facing == 1 || facing == 3)
                {
                    anim.Play("PlayerAttack_anim", 0);
                }
                else if (facing == 0)
                {
                    anim.Play("PlayerAttackU_anim", 0);
                }
                else if (facing == 2)
                {
                    anim.Play("PlayerAttackD_anim", 0);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HitTimer <= 0)
        {
            if (collision.collider.tag == "Enemy")
            {
                audio.sound.PlayOneShot(audio.hurt, 0.3f);
                HP -= Mathf.RoundToInt(10 / LevelVariables.Defense);
                HitTimer = 80;
            }
            else if (collision.collider.tag == "Boss")
            {
                audio.sound.PlayOneShot(audio.hurt, 0.3f);
                HP -= Mathf.RoundToInt(BossStats.Attack / LevelVariables.Defense);
                HitTimer = 80;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.tag == "Shop")
        {
            if (!inBoss)
            {
                if (!LevelVariables.runninground)
                {
                    E.SetActive(true);
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        ShopMenu.SetActive(true);
                        if (MaxHP == 5)
                        {
                            fight.enabled = true;
                            attack.enabled = false;
                            speed.enabled = false;
                            defense.enabled = false;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (!inBoss)
        {
            if (trig.tag == "Shop")
            {
                E.SetActive(false);
            }
        }
    }

    public void Attack()
    {
        audio.sound.PlayOneShot(audio.ready, 1);
        attacking = true;
    }

    public void EndAttack()
    {
        attacking = false;
        Explosion.GetComponent<SpriteRenderer>().enabled = false;
        Explosion.GetComponent<BoxCollider2D>().enabled = false;
        cooldown = 10;
    }

    public void BoomStick()
    {
        audio.sound.PlayOneShot(audio.shoot, 0.1f);
        Explosion.GetComponent<SpriteRenderer>().enabled = true;
        Explosion.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Death()
    {
        if (HP <= 0)
        {
            LevelVariables.Deaths += 1;
            Destroy(gameObject);
            Time.timeScale = 0.5f;
            gameOver.enabled = true;
        }
    }
}
