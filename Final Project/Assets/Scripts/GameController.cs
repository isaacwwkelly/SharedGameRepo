using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Player Stats
    [SerializeField] public int playerSpeed = 2;
    [SerializeField] public float jumpIntensity = 3;
    [SerializeField] public float climbIntensity = 1.5f;
    [SerializeField] public float timeTravelLimit;

    // Controller Aspects
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject newShadowPlayer;

    [SerializeField] public Vector2 returnVector = new Vector2(-5, 4);

    // Puzzle 
    public bool thisLvlHasButtons;
    public bool puzzleRequirementMet = false;
    private bool doorOpened = false;
    [SerializeField] public GameObject door;
    [SerializeField] private GameObject doorLock;
    private bool fading = false;
    [SerializeField] GameObject[] buttonsForDoor;


    // Start is called before the first frame update
    void Start()
    {
        timeTravelLimit = PlayerPrefs.GetFloat("returnVector", 10f);

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        if (!newShadowPlayer)
            newShadowPlayer = GameObject.FindGameObjectWithTag("shadowPlayer");

        if (thisLvlHasButtons)
            StartCoroutine(checkForRequirementsMet());
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleRequirementMet && !doorOpened)
        {
            doorOpened = true;
            fading = true;
            //StartCoroutine(FadeOut(doorLock));

            Debug.Log("DOOR UNLOCKED");
        }
        if (fading)
        {

            Color objectColor = doorLock.GetComponent<SpriteRenderer>().color;


            float fadeAmount = objectColor.a - (1f * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);

            doorLock.GetComponent<SpriteRenderer>().color = objectColor;

            if (objectColor.a <= 0)
            {
                fading = false;
                Destroy(doorLock);
            }
                
        }
    }

    IEnumerator checkForRequirementsMet()
    {
        while (puzzleRequirementMet == false)
        {
            if (checkButtons())
                puzzleRequirementMet = true;
            yield return new WaitForSeconds(1f);
        }
    }

    private bool checkButtons()
    {
        bool allArePressed = true;

        foreach (GameObject button in buttonsForDoor)
        {
            if (button.GetComponent<PressurePlatePress>().isPressed == false)
                allArePressed = false;
        }

        return allArePressed;
    }

    IEnumerator FadeOut(GameObject ObjectToFade)
    {
        Color objectColor = ObjectToFade.GetComponent<SpriteRenderer>().color;

        while (objectColor.a > 0)
        {
            float fadeAmount = objectColor.a - (0.005f * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);

            ObjectToFade.GetComponent<SpriteRenderer>().color = objectColor;
        }

        yield return new WaitForSeconds(0);
    }
}
