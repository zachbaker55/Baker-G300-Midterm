using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class Rotate : MonoBehaviour {
    // Properties
    [SerializeField] private float _bobHeight;
    public float BobHeight {
        get { return _bobHeight; }
        set { _bobHeight = value; }
    }

    [SerializeField] private float _bobSpeed;
    public float BobSpeed {
        get { return _bobSpeed; }
        set { _bobSpeed = value; }
    }

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed {
        get { return _rotationSpeed; }
        set { _rotationSpeed = value; }
    }
    
    // Fields

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update() {
        // Bobbing
        float newY = initialPosition.y + Mathf.Sin(Time.time * BobSpeed) * BobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        //Rotating
        transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
    }
    

}
