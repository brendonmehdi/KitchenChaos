using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    // Update is called once per frame

    private bool isWalking;
    private void Update()
    {

        //since we have 2 axis forward and back and left and right we can use a Vector2 and convert it later to a Vector3
        Vector2 inputVector = new Vector2(0, 0);

        //Input.GetKey ~ this method allows us to get the key of a button press as long as it pressed which is good for movement in game
        //Input.GetKeyDown ~ this method gets the key everytime its pressed so only once which is good to use for a character jump
        //in this case since were going to code in movement were going to use the GetKey
        if (Input.GetKey(KeyCode.W))
        {
            //for testing to see if something works we can send a message to the console using debug
            //Debug.Log("W is being pressed!");

            //moving the character
            inputVector.y = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //for testing to see if something works we can send a message to the console using debug
            //Debug.Log("W is being pressed!");

            inputVector.x = -1;

        }

        if (Input.GetKey(KeyCode.S))
        {
            //for testing to see if something works we can send a message to the console using debug
            //Debug.Log("W is being pressed!");

            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //for testing to see if something works we can send a message to the console using debug
            //Debug.Log("W is being pressed!");

            inputVector.x = 1;
        }


        //this will normalize our input vector so that if we strafe left or right meaning pressing (w) & (d) at the same time our character will not move faster then they should so all directions move at 
        //the same speed
        inputVector = inputVector.normalized;

        //we didnt make a vector 3 from the beginning because it doesnt make sense logicaly when we move were only going to be move up down left right so we should first get the input then actually move the object
        //this seperation is going to be usefull later
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * Time.deltaTime * moveSpeed; //this allows us to move at a constant speed so it doesnt matter the fps so the movement is always framrate independent


        //how to add rotation
        //transform.rotation but works with quaternions which is annoying
        //transform.eulerAngles meaning angles that go from 0-360
        //transform.lookAt() makes the transform look at a certain point
        //transform.forward has the normalized vector for the forward axis you can get it and set it this is what were goig to use to essentially rotate the player in the moveDir

        //transform.forward = moveDir;

        isWalking = moveDir != Vector3.zero; //this means were walking because the move direction is not zero meaning were moving vector3.zero is just (0, 0, 0) so if the movement is not 0 

        //were also going to use a math function called lerp to interpolate between 2 values so we have smooth turning slerp interpolates with a spherical rotation so were going to use slerp for this
        //examples since were rotating lerp interpolates between positions  
        //we also need a rotate speed to make the character rotate at a good pace
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        //the above makes it so over time our character will rotate towards the direction theyre moving in
    }

    //we have to make a function to return what the player is doing this class so we can tell the animator when the player is walking to play the walk animation
    public bool IsWalking()
    {
        return isWalking; //returning if true or false 
    }
}
