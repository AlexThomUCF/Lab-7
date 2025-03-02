using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    public Transform patrolRoute; // Parent containing waypoints
    public Transform player; // Player reference
    private NavMeshAgent agent;
    private Transform[] locations;
    private int currentLocation = 0;
    private bool chasingPlayer = false;
    Renderer render;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        render = GetComponent<Renderer>();

        if (patrolRoute == null || player == null)
        {
            Debug.LogError("Patrol Route or Player is not assigned in the inspector!");
            return;
        }

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (chasingPlayer)
        {
            agent.SetDestination(player.position); // Keep updating destination to follow player
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Length == 0) return;
        agent.SetDestination(locations[currentLocation].position);
        currentLocation = (currentLocation + 1) % locations.Length;
    }

    void InitializePatrolRoute()
    {
        locations = new Transform[patrolRoute.childCount];
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected - start chasing!");
            chasingPlayer = true;
            render.material.color = Color.red;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of range - resume patrol.");
            chasingPlayer = false;
            render.material.color = Color.yellow;
            MoveToNextPatrolLocation();
        }
    }
}
