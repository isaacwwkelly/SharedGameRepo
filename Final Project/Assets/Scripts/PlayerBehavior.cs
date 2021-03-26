using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float jumpIntensity;
    [SerializeField] private bool isGrounded;
    [SerializeField] private GameObject pastCharacter;

    private bool recordingMovements = false;
    private bool startOrStopTimeTravel = true;
    private bool itemsInList = true;
    private List<Vector3> movements = new List<Vector3>();
    private List<double> movement_times = new List<double>();

    // for player movement
    private Rigidbody2D rb2D;
    private Vector2 movement = Vector3.zero;
    //[SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        Move();
        Jump();

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Magnitude", movement.magnitude);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (startOrStopTimeTravel)
            {
                BeginTimeTravel();
                startOrStopTimeTravel = !startOrStopTimeTravel;
            }
            else
            {
                StopTimeTravel();
                startOrStopTimeTravel = !startOrStopTimeTravel;
            }
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb2D.velocity = new Vector2(moveBy, rb2D.velocity.y);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpIntensity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -2, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            isGrounded = false;
        }
        else if (collision.gameObject.tag == "climbable")
        {
            Physics2D.gravity = new Vector3(0, -9.8f, 0);
        }
    }

    public void BeginTimeTravel()
    {
        // Record the initial position and time tied to it
        movements.Add(transform.position);
        movement_times.Add(0);

        // Start the stopwatch
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        // Start recording the player movements
        recordingMovements = true;
        StartCoroutine(RecordMovements(stopWatch));
    }

    public void StopTimeTravel()
    {
        recordingMovements = false;
        StartCoroutine(ReplayMovements());
    }


    IEnumerator RecordMovements(Stopwatch sw)
    {
        UnityEngine.Debug.Log("STARTING the recording mode...");

        // Record the character movements until the time travel button is pressed again
        while (recordingMovements)
        {
            // If (this frame's) current position is a new position (!= to the most recent position), save that position to the movements List
            if (transform.position != movements[movements.Count - 1])
            {
                movements.Add(transform.position);
                movement_times.Add(sw.Elapsed.TotalSeconds);
            }
            yield return new WaitForSeconds(0);
        }

        sw.Reset();
        UnityEngine.Debug.Log("ENDING the recording mode... movements[] size = " + movements.Count);
    }

    IEnumerator ReplayMovements()
    {
        int index = 0;
        GameObject replayChar = Instantiate(pastCharacter, movements[0], Quaternion.identity);


        //transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        // Put the player back at the beginning of the time travel spot
        transform.position = movements[0];

        while (!recordingMovements && itemsInList)
        {

            //UnityEngine.Debug.Log("Event: " + movements[index] + " at " + movement_times[index]);

            if (movements.Count > index + 1)
            {
                //transform.position = movements[index];
                replayChar.transform.position = movements[index];
                yield return new WaitForSeconds((float)movement_times[index]/30); //(float)movement_times[index]  //CURRENT ISSUE HERE
                index += 1;

            }
            else
                itemsInList = false;
        }
        itemsInList = true;

        // Reset all the stuff
        Destroy(replayChar);
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        movements.Clear();
        movement_times.Clear();

        yield return new WaitForSeconds(0);
    }
}
