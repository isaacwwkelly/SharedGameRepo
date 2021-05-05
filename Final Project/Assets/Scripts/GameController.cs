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


    // Start is called before the first frame update
    void Start()
    {
        timeTravelLimit = PlayerPrefs.GetFloat("returnVector", 10f);


        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        if (!newShadowPlayer)
            newShadowPlayer = GameObject.FindGameObjectWithTag("shadowPlayer");
    }

    // Update is called once per frame
    void Update()
    {

    }

}
