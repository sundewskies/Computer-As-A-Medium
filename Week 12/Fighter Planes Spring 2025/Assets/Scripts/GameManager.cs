using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class GameManager : MonoBehaviour
{


    public GameObject powerupPrefab;
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject ShieldPrefab;


  



    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public GameObject audioPlayer;


    public TextMeshProUGUI livesText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI powerupText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score = 0;

 


   // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;

        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 2);
        InvokeRepeating("CreateCoin", 5, Random.Range(0.2f, 3.5f));
        


        StartCoroutine(SpawnPowerup());
        powerupText.text = "No powerups yet!";

        void CreatePowerup()
        {
            Instantiate(powerupPrefab,
                new Vector3(Random.Range(-horizontalScreenSize * 0.8f,
                horizontalScreenSize * 0.8f), verticalScreenSize, 0),
                Quaternion.identity);
        }

        IEnumerator SpawnPowerup()
        {
            float spawnTime = Random.Range(3, 5);
            yield return new WaitForSeconds(spawnTime);
            CreatePowerup();
            StartCoroutine(SpawnPowerup());
        }
    }


    // Update is called once per frame
    void Update()
    {
      

    }


    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

  void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 60; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
      




    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }



    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
        }
    }






    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        Debug.Log("Score: " + score);
        ScoreText.text = "Score: " + score;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}
