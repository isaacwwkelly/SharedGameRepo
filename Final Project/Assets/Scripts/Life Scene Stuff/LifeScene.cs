using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScene : MonoBehaviour
{

    [SerializeField] public Text lifeText;
    [SerializeField] private GameObject gameController;


    private void Awake()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifeScene());

    }

    private void Update()
    {
        lifeText.text = " x " + gameController.GetComponent<LifeCounter>().lifeCount;
    }

    IEnumerator lifeScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(gameController.GetComponent<LifeCounter>().levelCount);
        
    }
}
