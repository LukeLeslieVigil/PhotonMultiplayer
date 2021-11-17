using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    float currentSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = animator.GetCurrentAnimatorStateInfo(0).speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimationState();
    }

    void CheckAnimationState()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetTrigger("Run");
                animator.ResetTrigger("Walk");
            }
        }
        else
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Run");
        }
    }
}
