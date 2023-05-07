using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPosition : MonoBehaviour
{
    [SerializeField] private GameObject winText; //A GameObject variable containing the winText
    [SerializeField] private GameObject moveOnButton; //A GameObject variable containing the moveOnButton
    
    
    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false); //The winText variable containing the Game Object with the win text is set to be false
    }

    // Update is called once per frame
    void Update()
    {
        if (ThePieceBehavior.locked1 && ThePieceBehavior.locked2 && ThePieceBehavior.locked3 && ThePieceBehavior.locked4) // An if statement to check if every locked bool from the ThePieceBehavior script is true
        {
            winText.SetActive(true); //The winText variable containing the Game Object with the win text is set to active
            moveOnButton.SetActive(true); // The moveOnButton variable containing the Game Object with the button to move on is set to active
        }
    }

    //Code inspired by Youtube: Alexander Zotov: https://www.youtube.com/watch?v=7HEjCEncezs and https://www.youtube.com/watch?v=p7akGCRgBLA and modified to fit this project.
    
}
