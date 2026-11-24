using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public GameObject explosionPrefab;

    private GameManager gameManager;

    private bool bounceFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bounceFlag == false)
        {
            transform.Translate(new Vector3(3, 1, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(-3, 1, 0) * Time.deltaTime);
        }

        if (transform.position.x >= gameManager.horizontalScreenSize || transform.position.x <= -gameManager.horizontalScreenSize)
        {
            bounceFlag = !bounceFlag;
        }

        if (transform.position.y <= -gameManager.verticalScreenSize * 1.25f || transform.position.y >= gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(5);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(5);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
