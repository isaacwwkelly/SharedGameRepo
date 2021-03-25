using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    float dirX, moveSpeed = .5f;
    bool moveRight = true;

    private void Update()
    {
        if(transform.position.x > 1f)
        {
            moveRight = false;
        }
        if (transform.position.x < -1.315f)
        {
            moveRight = true;
        }
        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}
