using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _ammoAmount;
    public float AmmoAmount {
        get { return _ammoAmount; }
        set { _ammoAmount = value; }
    }

}
