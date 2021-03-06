using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimationState();
    }

    void CheckAnimationState()
    {
        float move = Input.GetAxis("Vertical");
        float halfSpeed = 0.5f;

        if(Input.GetAxis("Vertical") > 0)
        {
            animator.SetFloat("Speed", move * halfSpeed);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", move);
            }
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Shoot", true);
        }
        else
        {
            animator.SetBool("Shoot", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("SwapGun", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("SwapGun", 2);
        }
    }
}
