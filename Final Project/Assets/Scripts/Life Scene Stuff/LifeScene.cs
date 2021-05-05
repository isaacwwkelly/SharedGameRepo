using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScene : MonoBehaviour
{

    [SerializeField] public Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifeScene());

    }

    private void Update()
    {
        lifeText.text = " x " + LifeCounter.lifeCount;
    }

    IEnumerator lifeScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(LifeCounter.levelCount);
        
    }
}
