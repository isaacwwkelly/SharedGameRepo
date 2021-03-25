using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float jumpIntensity;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Left and right control on player
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 1) * speed * Time.deltaTime;

        // Allow player to jump, only if the player is grounded on a platform
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpIntensity, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            isGrounded = false;
        }
    }
}
