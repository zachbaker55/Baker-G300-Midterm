using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    // Properties
    [SerializeField] private GameObject activatableObject;
    [SerializeField] private float _lowerAmount = 0.3f;
    public float LowerAmount {
        get { return _lowerAmount; }
        set { _lowerAmount = value; }
    }

    // Fields
    private IActivatable activatable;
    private bool isLowered = false;

    private void Start() {
        activatable = activatableObject.GetComponent<IActivatable>();
        if (activatable == null) {this.enabled = false; }
    }

    // Methods
    void OnTriggerEnter(Collider collider) {
        Player player = collider.GetComponent<Player>();
        if (player != null && !isLowered) {
            transform.Translate(Vector3.down * LowerAmount);
            isLowered = true;
            activatable.Activate();
        }
    }
}
