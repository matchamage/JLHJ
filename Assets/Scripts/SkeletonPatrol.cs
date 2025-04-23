using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Transform player;
    public float detectionRange = 2f; 

    int m_CurrentWaypointIndex;
    bool isChasingPlayer = false;

    void Start ()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update ()
    {
        if (isChasingPlayer)
            return;

        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            StartCoroutine(ChasePlayer());
        }
        else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;
        navMeshAgent.SetDestination(player.position);

        float chaseDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < chaseDuration)
        {
            navMeshAgent.SetDestination(player.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isChasingPlayer = false;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
}