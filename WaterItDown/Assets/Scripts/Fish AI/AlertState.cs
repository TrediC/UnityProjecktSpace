using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private float searchTimer; 

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;


    }

    public void OnEnemyTrigger(Collider other)
    {
       
    }

    public void ToAlertState()
    {
        
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;

    }

    public void UpdateState()
    {
        Search();
        Look();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    private void Search()
    {
        enemy.Indicator.material.color = Color.yellow;
        enemy.navMeshAgent.isStopped = true;
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime; 

        if(searchTimer >= enemy.searchingDuration)
        {
            ToPatrolState();
        }


    }

    private void Look()
    {
        Debug.DrawRay(enemy.eyes.transform.position,
            enemy.eyes.transform.forward * enemy.sightRange,
            Color.green);

        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position,
            enemy.eyes.transform.forward, out hit, enemy.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }

    }

}
