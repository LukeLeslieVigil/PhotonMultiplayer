using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isRunningHash;
    int isJumpingHash;
    bool isRunning;
    bool isJumping;
    bool forwardPressed;
    bool jumpPressed;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunningForward");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = animator.GetBool(isRunningHash);
        forwardPressed = Input.GetKey("w");
        isJumping = animator.GetBool(isJumpingHash);
        jumpPressed = Input.GetKey("space");

        if (!isRunning && forwardPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if (isRunning && !forwardPressed)
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isJumping && jumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        else
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}
