using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivatable {
    public void Activate() {
        gameObject.SetActive(false);
    }
}
