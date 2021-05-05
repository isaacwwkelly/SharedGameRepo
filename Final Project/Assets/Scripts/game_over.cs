using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_over : MonoBehaviour
{
    public void Retry()
    {
        PlayerPrefs.DeleteKey("lifeCount");
        PlayerPrefs.DeleteKey("levelCount");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        PlayerPrefs.DeleteKey("lifeCount");
        PlayerPrefs.DeleteKey("levelCount");
        Debug.Log("Go to menu");
        SceneManager.LoadScene("John_Menu_Screen");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("lifeCount");
        PlayerPrefs.DeleteKey("levelCount");
    }
}
