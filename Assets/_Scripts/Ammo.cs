using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {
    private void OnTriggerEnter(Collider collider) {
        
        Player player = collider.GetComponent<Player>();
        if (player != null) {
            player.AmmoAmount += 3;
            Destroy(this.gameObject);
        }
    }
}
