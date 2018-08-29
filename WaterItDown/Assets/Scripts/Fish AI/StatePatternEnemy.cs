using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 50f;
    public float searchingDuration = 2f;
    public float detectRange = 5.0f;
    public Transform[] wayPoints;
    public Transform eyes;
    public Rigidbody rb;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Transform lastTargetPosition;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public PatrolState patrolState;


    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);

    }
    
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        currentState = patrolState;
    }
	
	void Update ()
    {
        DedectPlayer();
        currentState.UpdateState();
        print(currentState);
	}

    void DedectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRange);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Player") && currentState != chaseState)
                currentState = alertState;
            i++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnEnemyTrigger(other);
    }

}
