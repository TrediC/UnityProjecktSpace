using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 50f;
    public float searchingDuration = 2f;
    public float sightRange = 15f;
    public Transform[] wayPoints;
    public Transform eyes;
    public MeshRenderer Indicator;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Transform lastTargetPosition;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public PatrolState patrolState;


    [HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent; 


    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);

        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {

        currentState = patrolState; 
		
	}
	
	// Update is called once per frame
	void Update () {

        currentState.UpdateState();
       // Debug.Log(currentState);

	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnEnemyTrigger(other);
    }

}
