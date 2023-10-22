using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnemy : MonoBehaviour, IEnemy
{
    public void DoDamage(int damage) {
        Destroy(this.gameObject);
    }
}
