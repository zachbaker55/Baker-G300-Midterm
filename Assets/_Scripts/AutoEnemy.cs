using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoEnemy : MonoBehaviour, IEnemy {

    // Properties
    [SerializeField] private Player _player;
    [SerializeField] private float _damageDistance;

    // Fields
    private NavMeshAgent agent;

    // Methods
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        agent.SetDestination(_player.transform.position);
        if (Vector3.Distance(_player.transform.position, transform.position) <= _damageDistance) {
            _player.TakeDamage(1);
        }
    }

    public void DoDamage(int damage) {
        Destroy(this.gameObject);
    }

}
