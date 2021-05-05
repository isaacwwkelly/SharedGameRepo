using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] public GameObject gameOverPanel;
    public int lifeCount = 3;
    public int levelCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel = GameObject.Find("GameOver_UI");
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
        if (lifeCount != 0)
        {
            //load sounds
            lifeCount -= 1;
            PlayerPrefs.SetInt("lifeCount", lifeCount);
            //load scene
            SceneManager.LoadScene(5);
        }
        if(lifeCount <= 0 )
        {
            gameOverPanel.SetActive(true);
        }
    }


    public void addLeveltoCount()
    {
        levelCount += 1;
        PlayerPrefs.SetInt("levelCount", levelCount);

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("lifeCount");
        PlayerPrefs.DeleteKey("levelCount");
    }

}
