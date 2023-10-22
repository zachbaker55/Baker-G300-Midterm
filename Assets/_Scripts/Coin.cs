using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private void OnTriggerEnter(Collider collider) {
        
        Player player = collider.GetComponent<Player>();
        if (player != null) {
            GameManager.Instance.CoinAmount += 1;
            Destroy(this.gameObject);
        }
    }
}
