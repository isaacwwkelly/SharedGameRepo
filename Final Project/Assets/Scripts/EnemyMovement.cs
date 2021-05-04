using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float enemySpeed = 0.5f;
    public bool movementRight =true;
    public Transform playerPosition;


    
    // Update is called once per frame
    void Update()
    {
        //if flag is on, move right
        if(movementRight)
        {
            transform.Translate(2 * Time.deltaTime * enemySpeed, 0, 0);
            transform.localScale = new Vector2(0.156f, 0.156f);
        }
        else //move left
        {
            transform.Translate(-2 * Time.deltaTime * enemySpeed, 0, 0);
            transform.localScale = new Vector2(-0.156f, 0.156f);

        }



    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //if end of platform, partrol to other end
        if (trigger.gameObject.CompareTag("rightSidePlat"))
        {
            if(movementRight)
            {
                movementRight = false;
            }
            else
            {
                movementRight = true;
            }
        }
        //vice versa
        if (trigger.gameObject.CompareTag("leftSidePlat"))
        {
            if (movementRight == false)
            {
                movementRight = true;
            }
            else
            {
                movementRight = false;
            }
        }
    }
}
