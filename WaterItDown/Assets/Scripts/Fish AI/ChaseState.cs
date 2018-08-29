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

    public void ToChaseState(){}

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }
    
    private void Look()
    {
        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.detectRange);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Player"))
                enemy.chaseTarget = hitColliders[i].transform;
            else
                ToAlertState();

            i++;
        }


    }

    private void Chase()
    {
        enemy.rb.MovePosition(Vector3.LerpUnclamped(enemy.transform.position, enemy.chaseTarget.position,
            Time.deltaTime / Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position) * 2f));
    }



}
