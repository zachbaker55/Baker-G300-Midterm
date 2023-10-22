using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {

    // Properties
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _ammoText;

    [SerializeField] private float _coinAmount;
    public float CoinAmount {
        get { return _coinAmount; }
        set { _coinAmount = value; 
        _coinText.text = "Coins: " + _coinAmount; }
    }

    [SerializeField] private float _ammoAmount;
    public float AmmoAmount {
        get { return _ammoAmount; }
        set { _ammoAmount = value; 
        _ammoText.text  = "Ammo: " + _ammoAmount; }
    }

    

}
