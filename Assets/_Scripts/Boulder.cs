using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IActivatable {

    // Fields
    private Rigidbody rigidBody;
    private bool isActivated = false;


    // Methods
    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Activate() {
        rigidBody.useGravity = true;
        isActivated = true;
    }

    // Methods
    void OnTriggerEnter(Collider collider) {
        Player player = collider.GetComponent<Player>();
        if (player != null && isActivated) {
            player.TakeDamage(1);
        }
    }
    


}
