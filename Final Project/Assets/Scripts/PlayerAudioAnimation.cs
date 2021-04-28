using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioAnimation : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private AudioSource footStep;
    [SerializeField] private AudioClip jump;

    //NOTE: ADD SERIALIZE FIELD FOR MORE ANIMATIONS WHEN TIME

    // Start is called before the first frame update
    void Start()
    {
        footStep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("Jumping");
        }
    }

    private void Footstep()
    {
        footStep.Play();
    }
    private void Jumping()
    {
        
    }

}
