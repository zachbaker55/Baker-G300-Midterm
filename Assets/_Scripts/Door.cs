using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivatable {
    
    [SerializeField] private GameObject NavMesh1;
    [SerializeField] private GameObject NavMesh2;

    // Methods
    public void Activate() {
        gameObject.SetActive(false);
        NavMesh1.SetActive(false);
        NavMesh2.SetActive(true);
    }
}
