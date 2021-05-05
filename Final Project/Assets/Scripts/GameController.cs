using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Player Stats
    [SerializeField] public int playerSpeed = 2;
    [SerializeField] public float jumpIntensity = 3;
    [SerializeField] public float climbIntensity = 1.5f;

    // Controller Aspects
    [SerializeField] private PlayerBehavior playerBehavior;
    [SerializeField] private ShadowPlayerController shadowPlayerController;

    [SerializeField] public Vector2 returnVector = new Vector2(-5, 4);


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

}
