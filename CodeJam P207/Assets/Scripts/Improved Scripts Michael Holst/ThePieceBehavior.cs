using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePieceBehavior : MonoBehaviour
{
    [SerializeField] private Transform[] piecePlaces; //An array containing the Transform positions of the right places
    private Transform piecePlace; //A Transform variable that will contain the place variable needed for the correct piece

    private Vector2 initialPosition; //A Vector 2 variable containing the initial position of the puzzle piece 

    private Vector2 mousePosition; //A Vector 2 variable containing the Vector2 position of the mouseposition

    private float deltaX, deltaY; //These floats contains the x and y offsets of center of puzzle piece and the touch position.

    public static bool locked1; //This boolean will be set active to true, when the first puzzle piece is in the right location.
    public static bool locked2; //This boolean will be set active to true, when the second puzzle piece is in the right location.
    public static bool locked3; //This boolean will be set active to true, when the third puzzle piece is in the right location.
    public static bool locked4; //This boolean will be set active to true, when the fourth puzzle piece is in the right location.
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position; //The initial postion is set to the puzzle piece position, on Start
    }

    private void ChoosePiecePlace() // A method used to set the piecePlace variable to the needed place piece
    {
        switch (tag) //A switch statement to switch between the different cases, depending on the tag.
        {
            case "Brik1": //If the gameobject, that has this script attached has the tag of "Brik1", this switch statement will run
                piecePlace = piecePlaces[0]; //The piecePlace variable is set to Element 0 in the piecePlaces array
                break; //A break to end the case statement
            case "Brik2": //If the gameobject, that has this script attached has the tag of "Brik2", this switch statement will run
                piecePlace = piecePlaces[1]; //The piecePlace variable is set to Element 1 in the piecePlaces array
                break; //A break to end the case statement
            case "Brik3": //If the gameobject, that has this script attached has the tag of "Brik3", this switch statement will run
                piecePlace = piecePlaces[2]; //The piecePlace variable is set to Element 2 in the piecePlaces array
                break; //A break to end the case statement
            case "Brik4": //If the gameobject, that has this script attached has the tag of "Brik4", this switch statement will run
                piecePlace = piecePlaces[3]; //The piecePlace variable is set to Element 3 in the piecePlaces array
                break; //A break to end the case statement

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !locked1 && !locked2 && !locked3 && !locked4) // The if statement checks if the number of touches is above 0 and if the puzzle pieces locked bool is the reverse of true
        {
            var touch = Input.GetTouch(0); // Touch variable is set to Input.GetTouch which returns the touch input on the screen.
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); //A vector 2 variable called touchpos is set to the current touch position
            

            switch (touch.phase) //A switch statement switching between the different phases of a touch
            {
                case TouchPhase.Began: //This case statement checks when a finger has touched the screen
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos)) //This if statement checks if the Collider2D component on the gameobject is the same as the position of the pressed touch on the screen
                    {
                        deltaX = touchPos.x - transform.position.x; //Calculation of the offset between the touch position and the puzzle piece's position
                        deltaY = touchPos.y - transform.position.y; //Calculation of the offset between the touch position and the puzzle piece's position
                    }
                    break; //Break statement to end the case statement
                
                
                case TouchPhase.Moved: //This case statement checks when a finger is moved on the screen
                    ChoosePiecePlace(); //ChoosePiecePlace method is used to set the current puzzle place to the corresponding puzzle piece that will be placed there.
                    if (Mathf.Abs(transform.position.x - piecePlace.position.x) <= 0.5f && Mathf.Abs(transform.position.y - piecePlace.position.y) <= 0.5f) //The if statement checks if the x and y offset's of the puzzle piece is below or equal to 0.5. This will be used to see if the user has placed the puzzle piece close enough in the puzzle place. 
                    {
                        transform.position = new Vector2(piecePlace.position.x, piecePlace.position.y); //The puzzle piece's position will be set to the right puzzle place's position.
                        switch (tag) //A switch statement to check the tag.
                        {
                            case "Brik1": //If the gameobject that has the script attached to it has the tag of Brik1, this case statement will run
                                locked1 = true; //The locked1 bool will be set to true
                                break; //A break to end the case statement
                            case "Brik2": //If the gameobject that has the script attached to it has the tag of Brik2, this case statement will run
                                locked2 = true; //The locked2 bool will be set to true
                                break; //A break to end the case statement
                            case "Brik3": //If the gameobject that has the script attached to it has the tag of Brik3, this case statement will run
                                locked3 = true; //The locked3 bool will be set to true
                                break; //A break to end the case statement
                            case "Brik4": //If the gameobject that has the script attached to it has the tag of Brik4, this case statement will run
                                locked4 = true; //The locked4 bool will be set to true
                                break; //A break to end the case statement
                        }
                    }
                    else //An else statement to run if a finger moves on the screen and the x and y offset's are not below 0.5
                    {
                        transform.position = new Vector2(initialPosition.x, initialPosition.y); //The puzzle piece's position will be set to its initial position
                    }
                    break; //A break to end the case statement
            }
            
        }
    }

    private void OnMouseDown() //A method called when the user pressed a mouse button
    {
        if (!locked1 ||!locked2 ||!locked3 ||!locked4) //An if statement to check if any of the locked bools are the opposite of locked.
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x; // DeltaX will be set to the x offset of the mouseposition and the puzzle piece's position
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y; // DeltaY will be set to the x offset of the mouseposition and the puzzle piece's position
            
        }
    }

    private void OnMouseDrag() //A method called when the user clicked on a collider and is still holding down on the mouse button
    {
        if (!locked1 ||!locked2 ||!locked3 ||!locked4) //An if statement to check if any of the locked bools are the opposite of locked.
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //A vector2 variable called mousePosition will be set to the current mouseposition
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }

    private void OnMouseUp() //A method that will run when the user releases the mouse button
    {
        ChoosePiecePlace(); //ChoosePiecePlace method is used to set the current puzzle place to the corresponding puzzle piece that will be placed there.
        if (Mathf.Abs(transform.position.x-piecePlace.position.x) <=0.5f && Mathf.Abs(transform.position.y-piecePlace.position.y) <= 0.5f) //The if statement checks if the x and y offset's of the puzzle piece is below or equal to 0.5. This will be used to see if the user has placed the puzzle piece close enough in the puzzle place.
        {
            transform.position = new Vector2(piecePlace.position.x, piecePlace.position.y); //The puzzle piece's position will be set to the right puzzle place's position.

            switch (tag) //A switch statement to check the tag.
            {
                case "Brik1": //If the gameobject that has the script attached to it has the tag of Brik1, this case statement will run
                    locked1 = true; //The locked1 boolean will be set to true.
                    break; //A break to end the case statement
                case "Brik2": //If the gameobject that has the script attached to it has the tag of Brik2, this case statement will run
                    locked2 = true; //The locked2 boolean will be set to true.
                    break; //A break to end the case statement
                case "Brik3": //If the gameobject that has the script attached to it has the tag of Brik3, this case statement will run
                    locked3 = true; //The locked3 boolean will be set to true.
                    break; //A break to end the case statement
                case "Brik4": //If the gameobject that has the script attached to it has the tag of Brik4, this case statement will run
                    locked4 = true; //The locked4 boolean will be set to true.
                    break; //A break to end the case statement
            }
        }
        else //An else statement to run if the user releases the mouse buton and the x and y offset's are not below 0.5
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y); //The puzzle piece's position will be set to its initial position
        }
    }
    
    //Code inspired by Youtube: Alexander Zotov: https://www.youtube.com/watch?v=7HEjCEncezs and https://www.youtube.com/watch?v=p7akGCRgBLA and modified to fit this project.
    
}
