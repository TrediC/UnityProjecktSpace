using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState {


    private readonly StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void OnEnemyTrigger(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        // Tänne ei tuu mitään!
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Look()
    {
        Debug.DrawRay(enemy.eyes.transform.position,
            enemy.eyes.transform.forward * enemy.sightRange,
            Color.green);

        RaycastHit hit;
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eyes.transform.position;
        if (Physics.Raycast(enemy.eyes.transform.position,
            enemyToTarget, out hit, enemy.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
        }
        else
        {
            ToAlertState();

        }

    }

    private void Chase(){

        enemy.Indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();

    }



}
