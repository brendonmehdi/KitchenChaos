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

    //creates a new gameInput Reference
    [SerializeField] private GameInput gameInput;
    private void Update()
    {
     

        Vector2 inputVector = gameInput.getMovementVectorNormalized();
      





        //we didnt make a vector 3 from the beginning because it doesnt make sense logicaly when we move were only going to be move up down left right so we should first get the input then actually move the object
        //this seperation is going to be usefull later
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        //were going to use raycast which fires a laser from a certain point in a certain direction telling you if it hit something and this is what were going to use for collision
        //do it before we move

        //takes a vector 3 for the origin, a vector 3 for the direction and a float for the maxdistance, this returns a bool true if it hits something false if not
        //we used the players postion for the origin, the move dir for the direction and created a playwidth float for the distance
        /*        bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);*/ //put a explanation mark so opposite happens 

        //were going to use capsule cast instead for our character so the side of them doesnt clip through the object
        
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        //so when we use capsulecast we need the position the height of the player the player radius or width the movedir and the move distance and this form of object collision is much mor accurate 
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight , playerRadius, moveDir, moveDistance);



        if (!canMove)
        {
            //in here meaning we cannot move in this direction and were going to solve the problem in which when you move diagonally beside the wall you dont stay stuck there it 
            //the player should hug the wall and move slowly left or right like what would typically happen in a game 
            //attempt only X movement

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X axis
                moveDir = moveDirX;
            }
            else
            {
                //Else cannot move only the X so lets attempt Z movement 
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //can move only on the z
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }

        //now we made a if statement with our transform position code in there so we only move if the raycast is false meaning nothing was detected
        if (canMove)
        {
            transform.position += moveDir * moveDistance; //this allows us to move at a constant speed so it doesnt matter the fps so the movement is always framrate independent
        }
        




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
