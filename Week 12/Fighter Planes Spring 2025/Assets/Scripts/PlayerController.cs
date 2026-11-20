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
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    // Start is called before the first frame update
    void Start()
        
    {
        GameObject managerObject = GameObject.Find("GameManager");

        if (managerObject == null)
        {
            Debug.LogError("GameManager object not found in Hierarchy!");
            return;
        }

        gameManager = managerObject.GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager script is not attached!");
            return;
        }

shieldPrefab.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
   
    {
        if (hasShield)
        {
            hasShield = false;
            shieldPrefab.SetActive(false);
            return;
        }

        if (invincible)
        {
            return;
        }
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)

        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GetComponent<SpriteRenderer>().color =
    new Color(1, 1, 1, 0.4f);

            return;

        }

        invincible = true;
        StartCoroutine(InvincibleOff());

    }

        IEnumerator InvincibleOff()
        {
            yield return new WaitForSeconds(2f);
            invincible = false;
        GetComponent<SpriteRenderer>().color =
    new Color(1, 1, 1, 0.4f);
gameManager.PlaySound(2);
        }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
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
                case 1:
                    //Picked up speed
                    speed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    
                    //Picked up shield
                    if (!hasShield)
                    //Do I already have a shield?
                    //If yes: do nothing
                    //If not: activate the shield's visibility
                     {

                        hasShield = true;
                        shieldPrefab.SetActive(true);
                    }

                    gameManager.ManagePowerupText(4);
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
