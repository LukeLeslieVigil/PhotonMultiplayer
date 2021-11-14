using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;  
    Transform player;
    State currentState;

    // Animation
    Animator anim;    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new Idle(this.gameObject, agent, anim, player);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }
}
