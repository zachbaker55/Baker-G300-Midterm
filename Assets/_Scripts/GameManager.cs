using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {

    // Properties
    [SerializeField] private float _coinAmount;
    public float CoinAmount {
        get { return _coinAmount; }
        set { _coinAmount = value; 
        Debug.Log("Coins: " + _coinAmount); }
    }

    // Methods

}
