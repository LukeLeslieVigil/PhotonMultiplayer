using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    PhotonView PV;

    void Start()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            CheckAnimationState();
        }
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
