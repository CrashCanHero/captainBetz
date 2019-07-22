using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    public GameObject Shop, Player, Enemy;

    public int AmountToSpawn;

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void Awake()
    {
        AmountToSpawn = 5;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AddAttack()
    {
        Player.GetComponent<CharacterScript>().MaxHP -= 5;
        LevelVariables.AttackPower += 0.1f;
        LevelVariables.runninground = true;
        LevelVariables.Round += 1;
        Shop.SetActive(false);
    }

    public void AddDefense()
    {
        Player.GetComponent<CharacterScript>().MaxHP -= 5;
        LevelVariables.Defense += 0.1f;
        LevelVariables.runninground = true;
        LevelVariables.Round += 1;
        Shop.SetActive(false);
    }

    public void AddSpeed()
    {
        Player.GetComponent<CharacterScript>().MaxHP -= 5;
        LevelVariables.Speed += 1;
        LevelVariables.runninground = true;
        LevelVariables.Round += 1;
        Shop.SetActive(false);
    }

    public void ToTheBoss()
    {
        SceneManager.LoadScene(2);
    }

    public void NewRound()
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            int neg = Random.Range(-3, 3);
            Vector3 pos = RandomCircle(transform.position, 40 + Random.Range(0, 40) * neg);

            Instantiate(Enemy, pos, Quaternion.identity);
        }

        CharacterScript.timer = 80;
        AmountToSpawn += 1;
    }
}
