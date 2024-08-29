using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    private PlayerInputActions inputActions;
    private void Awake()
    {
        //playerinputactions object created
        inputActions = new PlayerInputActions();

        //enabled action map
        inputActions.Player.Enable();


    }

    public Vector2 getMovementVectorNormalized()
    {
        //since we have 2 axis forward and back and left and right we can use a Vector2 and convert it later to a Vector3
        //below is basically the only code we need now to get our character moving
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>(); //returns a vector 2 thats going to be our input vector

        //Input.GetKey ~ this method allows us to get the key of a button press as long as it pressed which is good for movement in game
        //Input.GetKeyDown ~ this method gets the key everytime its pressed so only once which is good to use for a character jump
        //in this case since were going to code in movement were going to use the GetKey

        //everything below gets commented out because its replaced with our playerinputactions
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //for testing to see if something works we can send a message to the console using debug
        //    //Debug.Log("W is being pressed!");

        //    //moving the character
        //    inputVector.y = 1;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    //for testing to see if something works we can send a message to the console using debug
        //    //Debug.Log("W is being pressed!");

        //    inputVector.x = -1;

        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    //for testing to see if something works we can send a message to the console using debug
        //    //Debug.Log("W is being pressed!");

        //    inputVector.y = -1;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    //for testing to see if something works we can send a message to the console using debug
        //    //Debug.Log("W is being pressed!");

        //    inputVector.x = 1;
        //}


        //this will normalize our input vector so that if we strafe left or right meaning pressing (w) & (d) at the same time our character will not move faster then they should so all directions move at 
        //the same speed
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
