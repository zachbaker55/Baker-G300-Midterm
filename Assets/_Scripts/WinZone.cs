using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour {

    [SerializeField] private GameObject _winText;

    private void OnTriggerEnter(Collider collider) {
        
        Player player = collider.GetComponent<Player>();
        if (player != null) {
            _winText.SetActive(true);
        }
    }
}
