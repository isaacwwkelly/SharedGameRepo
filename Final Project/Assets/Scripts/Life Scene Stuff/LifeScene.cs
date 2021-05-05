using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScene : MonoBehaviour
{

    [SerializeField] public Text lifeText;
    [SerializeField] private GameObject gameController;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject bgm;

    private void Awake()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController.GetComponent<GameObject>();
        if (gameController.GetComponent<LifeCounter>().lifeCount != 0)
        {
            StartCoroutine(lifeScene());
        }
    }

    private void Update()
    {
        lifeText.text = " x " + gameController.GetComponent<LifeCounter>().lifeCount;

        if(gameController.GetComponent<LifeCounter>().lifeCount == 0)
        {
            gameOver();
        }
    }

    IEnumerator lifeScene()
    {
        
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene(gameController.GetComponent<LifeCounter>().levelCount);
        
    }

    public void gameOver()
    {
        AudioSource s = bgm.GetComponent<AudioSource>();
        s.Pause();
        gameOverPanel.SetActive(true);
    }
}
