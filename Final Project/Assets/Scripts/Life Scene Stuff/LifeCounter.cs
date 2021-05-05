using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour
{
    public static int lifeCount = 3;
    public static int levelCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        lifeCount = PlayerPrefs.GetInt("lifeCount", lifeCount);
        levelCount = PlayerPrefs.GetInt("levelCount", levelCount);
        
    }

    // Update is called once per frame
    void Update()
    {
        //ONLY CALL THIS SCRIPT WHEN THE PLAYER GETS HIT/DEAD
        //SceneManager.LoadScene(levelCount);
        
    
    
    }

    //if hit object/falls
    //life - 1
    //call is life taken

    public void isLifeTaken()
    {
        //load sounds
        lifeCount -= 1;
        PlayerPrefs.SetInt("lifeCount", lifeCount);
        //load scene
        SceneManager.LoadScene(5);
    }


    public void addLeveltoCount()
    {
        levelCount += 1;
        PlayerPrefs.SetInt("levelCount", levelCount);

    }

}
