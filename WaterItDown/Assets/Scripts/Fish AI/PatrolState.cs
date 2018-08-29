using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;
    

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Patrol();
        Look();
        
    }

    public void OnEnemyTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }

    }

    public void ToPatrolState()
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;


    }

    public void ToChaseState()
    {
        Debug.Log("Lähdetään jahtaamaan");
        enemy.currentState = enemy.chaseState;


    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Patrol()
    {
        enemy.Indicator.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWayPoint].position;
        enemy.navMeshAgent.isStopped = false;

        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance 
            && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;

        }

    }

    private void Look()
    {
        Debug.DrawRay(enemy.eyes.transform.position, 
            enemy.eyes.transform.forward*enemy.sightRange, 
            Color.green);

        RaycastHit hit;
        if(Physics.Raycast(enemy.eyes.transform.position, 
            enemy.eyes.transform.forward, out hit, enemy.sightRange) 
            && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }

    }

}
