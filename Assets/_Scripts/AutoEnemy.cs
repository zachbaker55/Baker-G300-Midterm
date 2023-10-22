using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoEnemy : MonoBehaviour, IEnemy {

    // Properties
    [SerializeField] private Player player;

    // Fields
    private NavMeshAgent agent;

    // Methods
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        agent.SetDestination(player.transform.position);
    }

    public void DoDamage(int damage) {
        Destroy(this.gameObject);
    }
}
