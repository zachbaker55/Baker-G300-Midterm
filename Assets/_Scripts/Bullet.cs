using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float _despawnTimer;
    private Player _player;
    private float _currentTime;

    private float _shootSpeed;
    private int _damage;

    private Rigidbody _rigidBody;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() {
        UpdateTimer();
    }

    public void Init(Player player, int damage) {
        _player = player;
        _currentTime = 0;
        _damage = damage;
        _shootSpeed = player.BulletSpeed;
        _rigidBody.velocity = _shootSpeed * _player.transform.forward.normalized;
    }

    private void UpdateTimer() {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _despawnTimer) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
        if (enemy != null) {
            enemy.DoDamage(_damage);
        }
        Destroy(gameObject);
    }

    private void OnDestroy() {
        _player.BulletCount--;
    }

}
