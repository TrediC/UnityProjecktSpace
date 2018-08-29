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
        enemy.currentState = enemy.chaseState;
    }
    
    void Patrol()
    {
        enemy.rb.MovePosition(Vector3.LerpUnclamped(enemy.transform.position, enemy.wayPoints[nextWayPoint].position, 
            Time.deltaTime / Vector3.Distance(enemy.transform.position, enemy.wayPoints[nextWayPoint].position)));
       
        if(Vector3.Distance(enemy.transform.position, enemy.wayPoints[nextWayPoint].position) < 1)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
        }
    }
}
