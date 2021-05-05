using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePress : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] public bool isPressed = false;
    [SerializeField] GameObject actualPlate;
    [SerializeField] public bool stickyButton;  //true if the buttons only need to be pressed once to stay down. False if buttons need to be held down to stay green

    [SerializeField] public Color currentColor;

    public Color greenColor = new Color(74f, 200f, 63f, 255f);
    public Color redColor = new Color(200f, 74f, 63f, 255f);

    private void Start()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        startColor();
    }

    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "shadowPlayer")
        {

            isPressed = true;
            // make plate green
            currentColor = greenColor;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!stickyButton && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "shadowPlayer"))
        {

            isPressed = false;
            // make plate green
            currentColor = redColor;
        }
    }

    private void startColor()
    {
        // make plate red from the start
        currentColor = redColor;
    }
}
