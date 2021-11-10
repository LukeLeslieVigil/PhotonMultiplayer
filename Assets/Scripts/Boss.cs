using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    private GameObject[] players;
    private float distance;
    private float currentMinDistance = 1000000;

    private float stopDistance = 7.5f;
    private bool stopMoving = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        findTarget();
    }

    void findTarget()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            distance = Vector3.Distance(transform.position, players[i].transform.position);
            if (distance < currentMinDistance)
            {
                target = players[i].transform;
                stopMoving = false;
                updateMovement();
                //Debug.Log(target + " " + currentMinDistance + " " + distance);
                currentMinDistance = distance;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            currentMinDistance = Vector3.Distance(transform.position, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            findTarget();
            //currentMinDistance = 1000000;
            updateMovement();
        }
    }

    private void updateMovement()
    {
        if (stopMoving == false)
        {
            Vector3 direction = target.position - transform.position;
            rb.transform.LookAt(target.transform);
            direction.Normalize();
            movement = direction;
            rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));            
            if (Vector3.Distance(transform.position, target.transform.position) < stopDistance)
            {
                stopMoving = true;
            }
        }
        if (stopMoving == true)
        {
            Vector3 direction = target.position - transform.position;
            rb.transform.LookAt(target.transform);
            direction.Normalize();
            movement = direction;
            rb.MovePosition((Vector3)transform.position + (direction * 0f * Time.deltaTime));
        }
    }
}