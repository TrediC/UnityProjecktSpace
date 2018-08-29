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
        MoveToContactPoint();
    }

    private void MoveToContactPoint()
    {
        searchTimer = Time.time + enemy.searchingDuration;
        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.detectRange * 0.75f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                enemy.rb.MovePosition(Vector3.LerpUnclamped(enemy.transform.position, hitColliders[i].transform.position,
            Time.deltaTime / Vector3.Distance(enemy.transform.position, hitColliders[i].transform.position)));
                enemy.chaseTarget = hitColliders[i].transform;
                ToChaseState();
            }
            if(searchTimer > Time.time)
            {
                ToPatrolState();
            }
            i++;
        }
    }
}
