using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private string secretKey = "gdhjtbudindustbmzwkopsderfgtyhkz";
    public string addScoreURL = "http://triangle.doucl.com/defaultdance.php?";
    public string highscoreURL = "http://triangle.doucl.com/queryv2.php?q=score";
    WWW wwwHighscores;

    public InputField input;

    public int Seconds;
    public string name;

    public IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(Seconds);
        Application.Quit();
    }

    public void RunCheck()
    {
        if (EndingScript.ok())
        {
            name = input.text;
            PostScores(input.text, LevelVariables.Score, getIP(), LevelVariables.Deaths);
            StartCoroutine(CloseGame());
        }
        else
        {
            Application.Quit();
        }
    }

    //returns the users IP
    public static string getIP()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        return externalip;
    }

    public void Start()
    {
        addScoreURL = "http://triangle.doucl.com:8219/defaultdance.php?";
    }

    //This is where we post

    public void PostScores(string name, int score, string ip, int deaths)
    {
        ip = getIP();
        string hash = Md5Sum(name + ip + score + deaths + secretKey);
        string iphash = Md5Sum(ip + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("ip", ip);
        form.AddField("hash", hash);
        form.AddField("deaths", deaths);
        wwwHighscores = new WWW(addScoreURL, form);

        Debug.Log(wwwHighscores.error);
    }

    // This is used to create a md5sum - so that we are sure that only legit scores are submitted.
    // We use this when we post the scores.
    // This should probably be placed in a seperate class. But is placed here to make it simple to understand.

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes

        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)

        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
