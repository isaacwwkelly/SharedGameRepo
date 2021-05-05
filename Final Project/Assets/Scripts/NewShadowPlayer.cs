using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShadowPlayer : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerBehavior playerBehavior;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator shadowAnimator;

    private SpriteRenderer shadowSpriteRenderer;

    // Shadow Player Movement
    //[SerializeField] private bool isGrounded = false;


    // Time travel stuff
    [SerializeField] public bool recordingMovements = false;
    [SerializeField] public bool replayingMovements = false;
    private bool startOrStopTimeTravel = true;
    private List<Vector3> playerMovements = new List<Vector3>();
    private List<Quaternion> playerRotations = new List<Quaternion>();
    private List<float> playerMvmntTimes = new List<float>();
    private List<bool> playerRunning = new List<bool>();
    private bool recordingPhases = false;
    [SerializeField] private float phaseTime = 0.01f;
    private float startingTime;
    [SerializeField] private Vector3 restingPosition;



    // Start is called before the first frame update
    void Start()
    {

        // Double check if the GameController.cs is not attached to the player in Unity
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        if (!playerBehavior)
            playerBehavior = player.GetComponent<PlayerBehavior>();
        if (!playerSpriteRenderer)
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

        shadowSpriteRenderer = GetComponent<SpriteRenderer>();

        if (!playerAnimator)
            playerAnimator = player.GetComponent<Animator>();
        if (!shadowAnimator)
            shadowAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


        // Right now the time travel button is Return, can be changed later
        // Time Travel Controller logic right here
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (startOrStopTimeTravel) // if it's the first time pressing the time travel button, this will trigger the recording FOR the shadow
            {
                // Record the initial position and time tied to it
                //movements3.Add(player.transform.position);
                startingTime = Time.time;

                // Start recording the player movements by setting this to true
                // This will record the player's position every `phaseTime` seconds and then the last position in movements3[] and the related 
                //  timestamps in movement_times2[].
                //recordingMovements = true;
                recordingPhases = true;
                StartCoroutine(RecordPhases());

                startOrStopTimeTravel = !startOrStopTimeTravel;

            }

            else // If it's the second time hitting the time travel button, which will trigger the replay OF the shadow
            {
                // Stop recording
                recordingPhases = false;

                playerMovements.Add(player.transform.position);
                playerRotations.Add(player.transform.rotation);
                playerRunning.Add(playerAnimator.GetBool("running"));
                playerMvmntTimes.Add(phaseTime);

                //Debug.Log("movements3 count: " + movements3.Count + "  \tmovement_times2 count: " + movement_times2.Count);
                //Debug.Log("PRINTING:\n\t    movements3[0]: " + movements3[0]+ "  \tmovement_times2[0]: " + movement_times2[0]);
                //for (int i = 0; i < playerMovements.Count; i++)
                //{
                //    Debug.Log("movements3[" + i + "]: " + playerMovements[i] + "  \tmovement_times2[" + i + "]: " + playerMovements[i]);
                //    Debug.Log(playerRunning[i]);
                //}

                StartCoroutine(ReplayPhases());

                startOrStopTimeTravel = !startOrStopTimeTravel;
            }
        }
    }

    IEnumerator RecordPhases()
    {
        while (recordingPhases)
        {
            playerMvmntTimes.Add(phaseTime);
            playerMovements.Add(player.transform.position);
            playerRotations.Add(player.transform.rotation);
            // playerSprites.Add(playerSpriteRenderer.sprite);
            playerRunning.Add(playerAnimator.GetBool("running"));
            yield return new WaitForSeconds(phaseTime);
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator ReplayPhases()
    {
        for (int i = 0; i < playerMovements.Count; i++)
        {
            yield return new WaitForSeconds(playerMvmntTimes[i]);
            //shadowSpriteRenderer.sprite = playerSprites[i];
            shadowAnimator.SetBool("running", playerRunning[i]);
            transform.position = playerMovements[i];
            transform.rotation = playerRotations[i];
        }

        playerMovements.Clear();
        playerRotations.Clear();
        playerMvmntTimes.Clear();
        playerRunning.Clear();

        transform.position = restingPosition;
        shadowAnimator.SetBool("running", false);
        yield return new WaitForSeconds(0);

    }
}
