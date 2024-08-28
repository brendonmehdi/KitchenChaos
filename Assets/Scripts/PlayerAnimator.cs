using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking"; //the same name we made for the bool we made in the animation tab of the game engine
    //we used a const instead of just putting the string in the setBool below because if we use the wrong string we wont get any errors at all and the animation wont work
    //becauase a string is a string and to the ide thats correct at least with a constant variable if we misspell the variable we get some sort of error however you need to make sure
    //whats stored in the const has the correct name 


    [SerializeField] private Player player; //dragged a ref of our player to this in the game engine


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();//now we have a ref to our animator component 
    }

    private void Update()
    {
        //we want to do this on every single frame to always check if the player is walking so we put it in update()
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
