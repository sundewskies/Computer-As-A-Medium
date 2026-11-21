using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    private int weaponType;
    private bool invincible = false;   // new
    private bool hasShield = false;
    
    



    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;




    // Start is called before the first frame update
    void Start()

    {
       
      gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
            enabled = false;
            return;
        }

        lives = 3;
        speed = 5.0f;

        gameManager.ChangeLivesText(lives);

        if (shieldPrefab != null)
        {
            shieldPrefab.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
   
    {
        if (hasShield == true)
        {
            hasShield = false;
            shieldPrefab.SetActive(false);
            gameManager.ManagePowerupText(0);
            gameManager.PlaySound(2);
            return;   // leave, no life lost
        }


        if (invincible)
        {
            return;
        }
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        invincible = true;

        StartCoroutine(InvincibleOff());
        GetComponent<SpriteRenderer>().color =
                        new Color(1, 1, 1, 0.4f);
       
if (lives == 0)
       {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            return;

        }
       

    }

        IEnumerator InvincibleOff()
        {
            yield return new WaitForSeconds(2f);
            invincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        
        gameManager.PlaySound(2);

    }

   
 private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 4:
                    //
                    //Picked up shield
                    //Picked up shield
                    if (hasShield == false)   // do I have one?
                    {
                        hasShield = true;
                        shieldPrefab.SetActive(true);
                        gameManager.ManagePowerupText(4);
                        Debug.Log("Shield activated!");
                    }
                        break;
            }
        }
    }








    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }
void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        if (gameManager == null)
        {
            Debug.LogError("GameManager is missing!");
            return;
        }

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

    }
}
