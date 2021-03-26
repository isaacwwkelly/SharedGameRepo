using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    
    [SerializeField] private AudioSource footStep;

    //NOTE: ADD SERIALIZE FIELD FOR MORE ANIMATIONS WHEN TIME

    // Start is called before the first frame update
    void Start()
    {
        footStep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Footstep()
    {
        footStep.Play();
    }


}
