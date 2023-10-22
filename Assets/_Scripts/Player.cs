using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


    // Properties
    [SerializeField] private float _ammoAmount;
    public float AmmoAmount {
        get { return _ammoAmount; }
        set { _ammoAmount = value; 
        GameManager.Instance.AmmoAmount = _ammoAmount; }
    }

    [SerializeField] private float _bulletSpeed;
    public float BulletSpeed {
        get { return _bulletSpeed; }
        set { _bulletSpeed = value; }
    }

    [SerializeField] private int _maxBulletCount;
    public int MaxBulletCount {
        get { return _maxBulletCount; }
    }
    private int _bulletCount = 0;
    public int BulletCount {
        get { return _bulletCount; }
        set { _bulletCount = value; }
    }


    // Methods

    private void Start() {
        AmmoAmount = _ammoAmount;
    }


    public void TakeDamage(int damage) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
