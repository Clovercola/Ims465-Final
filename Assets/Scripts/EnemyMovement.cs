using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Code here is based on the Unity Manual's "Making an Agent Patrol Between a Set of Points" Article.
public class EnemyMovement : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        NextPoint();
    }

    private void NextPoint()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update(){
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            NextPoint();
        }
    }
}
