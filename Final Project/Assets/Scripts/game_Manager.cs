using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_Manager : MonoBehaviour
{
    public static bool gameEnded;
    public GameObject gameOverUI;

    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }
        /* // For when the players lives run out
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
        */
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("GAME OVER");
        // disable player movement script here, add this if statement wherever the movement happens
        /*
         * if(game_Manager.gameEnded)
           {
                this.enabled = false;   
                return;
           }
         */

        gameOverUI.SetActive(true);

    }
}
