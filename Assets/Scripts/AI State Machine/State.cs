using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public Transform target;
    private Rigidbody rb;
    private GameObject[] players;
    private float distance;
    private float currentMinDistance = 1000000;

    public enum STATE
    {
        IDLE, SEARCH, ATTACK
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };    

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    float visDist = 30f;
    float visAngle = 90.0f;
    float attackDist = 10f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < attackDist)
        {
            return true;
        }
        return false;
    }

    public void updateMovement()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            distance = Vector3.Distance(agent.transform.position, players[i].transform.position);
            if (distance < currentMinDistance)
            {
                target = players[i].transform;                
                currentMinDistance = distance;
            }
        }
    }
}

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {        
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Attack(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 10)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Search : State
{
    int currentIndex = -1;

    public Search(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.SEARCH;
        agent.speed = 1;
        agent.isStopped = false;
    }

    public override void Enter()
    {        
        //anim.SetTrigger("isWalking");
        currentIndex = 0;
        base.Enter();
    }

    public override void Update()
    {
        updateMovement();
        if (CanSeePlayer())
        {
            nextState = new Attack(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class Attack : State
{
    //float rotationSpeed = 2.0f;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        //anim.SetTrigger("isPunching");
        base.Enter();
    }

    public override void Update()
    {
        agent.transform.LookAt(player.transform);
        var distance = Vector3.Distance(player.position, npc.transform.position);        
        if (distance <= 10f)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isPunching");
        //Debug.Log("You've been caught! Game over.");
        //Time.timeScale = 0;
        base.Exit();
    }
}