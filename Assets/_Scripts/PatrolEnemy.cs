using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour {
    // Properties
    [SerializeField] private Player _player;
    [SerializeField] private float _damageDistance;
    [SerializeField] private float _spotDistance;
    [SerializeField] private List<Transform> _patrolPoints;

    // Fields
    private NavMeshAgent agent;
    private int phase = 0;

    // Methods
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {

        float playerDist = Vector3.Distance(_player.transform.position, transform.position);
        if (playerDist <= _spotDistance) {
            agent.SetDestination(_player.transform.position);
        } else {
            agent.SetDestination(_patrolPoints[phase].transform.position);
        }
        if (playerDist <= _damageDistance) {
            _player.TakeDamage(1);
        }
        if (Vector3.Distance(_patrolPoints[phase].transform.position, transform.position) <= 0.1) {
            phase++;
            if (phase == _patrolPoints.Count) phase = 0;
        }
    }

    public void DoDamage(int damage) {
        Destroy(this.gameObject);
    }
}