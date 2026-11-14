using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    private bool bounceFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bounceFlag == false)
        {
            transform.Translate(new Vector3(3, -1, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(-3, -1, 0) * Time.deltaTime);
        }

        if (transform.position.x > 9.5f || transform.position.x < -9.5f)
        {
            bounceFlag = !bounceFlag;
        }

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
