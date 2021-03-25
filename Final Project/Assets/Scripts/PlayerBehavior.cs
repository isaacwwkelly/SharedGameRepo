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
        
        // record the character movements until the time travel button is pressed again
        while (recordingMovements)
        {
            //if (this frame's) current position is a new position (!= to the most recent position), save that position to the movements List
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

        //foreach (Vector3 pos in movements)
        //{
        //    UnityEngine.Debug.Log("Event: " + pos + " at " + movement_times[index]);
        //    index += 1;
        //}

        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
        while (!recordingMovements && itemsInList)
        {
            
            UnityEngine.Debug.Log("Event: " + movements[index] + " at " + movement_times[index]);

            if (movements.Count > index + 1)
            {
                transform.position = movements[index];
                yield return new WaitForSeconds(0); //(float)movement_times[index]
                index += 1;
                
            }
            else
                itemsInList = false;
        }
        itemsInList = true;

        //reset all the stuff
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        movements.Clear();
        movement_times.Clear();

        yield return new WaitForSeconds(0);
    }
}
