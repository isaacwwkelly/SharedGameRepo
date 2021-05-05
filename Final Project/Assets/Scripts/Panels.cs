using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Panels : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject nextLevelPanel;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject winPanel;

    public int nextLevel = 0;

    private void Awake()
    {
        nextLevelPanel = GameObject.Find("Next Level Panel");
        deathPanel = GameObject.Find("Death Panel");
        winPanel = GameObject.Find("Win Panel");


     
            nextLevelPanel.SetActive(false);
            deathPanel.SetActive(false);
            winPanel.SetActive(false);
        
    }
   


    public void ifDeath()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
        //button calls
        //load scenes depending on which call
    }

    public void ifWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
     
    public void ifNextLevel()
    {
        nextLevel += 1;
        nextLevelPanel.SetActive(true);
        Time.timeScale = 0;
        SceneManager.LoadScene(nextLevel);

    }


    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
