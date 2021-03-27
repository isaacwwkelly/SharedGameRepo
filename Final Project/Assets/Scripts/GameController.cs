using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Player Stats
    [SerializeField] public int playerSpeed = 2;
    [SerializeField] public float jumpIntensity = 3;

    // Controller Aspects
    [SerializeField] private PlayerBehavior playerBehavior;
    [SerializeField] private ShadowPlayerController shadowPlayerController;


    // Start is called before the first frame update
    void Start()
    {
        if (!playerBehavior)
            playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        if (!shadowPlayerController)
            shadowPlayerController = GameObject.FindGameObjectWithTag("shadowPlayer").GetComponent<ShadowPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //public void BeginTimeTravel()
    //{
    //    // Record the initial position and time tied to it
    //    movements.Add(rb2DT.position);
    //    movement_times.Add(0);

    //    // Start the stopwatch
    //    //var stopWatch = new Stopwatch();
    //    //stopWatch.Start();

    //    // Start recording the player movements
    //    recordingMovements = true;

    //    // Option 1
    //    //StartCoroutine(RecordMovements(stopWatch));


    //}

    //public void StaticRecordMovements()
    //{
    //    UnityEngine.Debug.Log("adding");
    //    if (rb2DT.position.x != movements[movements.Count - 1].x && rb2DT.position.y != movements[movements.Count - 1].y)
    //    {
    //        movements.Add(rb2DT.position);
    //        movement_times.Add(Time.deltaTime);
    //    }
    //}

    //public void StopTimeTravel()
    //{
    //    recordingMovements = false;

    //    // Option 1
    //    //StartCoroutine(ReplayMovements());

    //    // Option 2
    //    StaticReplayMovements();

    //}

    //public void StaticReplayMovements()
    //{
    //    int index_i = 0;
    //    //GameObject replayChar = Instantiate(ShadowPlayer, movements[0], Quaternion.identity);

    //    // Put the player back at the beginning of the time travel spot
    //    shadowRb2DT.position = movements[0];

    //    if (!recordingMovements && itemsInList)
    //    {
    //        //UnityEngine.Debug.Log("Event: " + movements[index] + " at " + movement_times[index]);

    //        if (movements.Count > index_i + 1)
    //        {
    //            //shadowRb2DT.position = movements[index];



    //            shadowRb2DT.position = movements[index_i];
    //            new WaitForSeconds((float)movement_times[index_i]); //(float)movement_times[index]  //CURRENT ISSUE HERE
    //            index_i += 1;

    //        }
    //        else
    //            itemsInList = false;
    //    }
    //    itemsInList = true;

    //    // Reset all the stuff
    //    //Destroy(replayChar);
    //    MoveBack();
    //    //transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    //    movements.Clear();
    //    movement_times.Clear();

    //}

    //private void MoveBack()
    //{
    //    shadowRb2DT.position = new Vector2(-3, 1);
    //}

    //IEnumerator RecordMovements(Stopwatch sw)
    //{
    //    UnityEngine.Debug.Log("STARTING the recording mode...");

    //    // Record the character movements until the time travel button is pressed again
    //    while (recordingMovements)
    //    {
    //        // If (this frame's) current position is a new position (!= to the most recent position), save that position to the movements List
    //        if (rb2DT.position.x != movements[movements.Count - 1].x && rb2DT.position.y != movements[movements.Count - 1].y)
    //        {
    //            movements.Add(transform.position);
    //            movement_times.Add(sw.Elapsed.TotalSeconds);
    //        }
    //        yield return new WaitForSeconds(0);
    //    }

    //    sw.Reset();
    //    UnityEngine.Debug.Log("ENDING the recording mode... movements[] size = " + movements.Count);
    //}

    //IEnumerator ReplayMovements()
    //{
    //    int index = 0;
    //    GameObject replayChar = Instantiate(ShadowPlayer, movements[0], Quaternion.identity);


    //    //transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

    //    // Put the player back at the beginning of the time travel spot
    //    transform.position = movements[0];

    //    while (!recordingMovements && itemsInList)
    //    {

    //        //UnityEngine.Debug.Log("Event: " + movements[index] + " at " + movement_times[index]);

    //        if (movements.Count > index + 1)
    //        {
    //            //transform.position = movements[index];
    //            replayChar.transform.position = movements[index];
    //            yield return new WaitForSeconds((float)movement_times[index]); //(float)movement_times[index]  //CURRENT ISSUE HERE
    //            index += 1;

    //        }
    //        else
    //            itemsInList = false;
    //    }
    //    itemsInList = true;

    //    // Reset all the stuff
    //    Destroy(replayChar);
    //    transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    //    movements.Clear();
    //    movement_times.Clear();

    //    yield return new WaitForSeconds(0);
    //}
}
