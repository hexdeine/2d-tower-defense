using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    public void Awake() {
        maxHP = EnemySpawner.Instance.maxHP;
        currentHP = maxHP;
    }

    public void DecreaseHealth(int damage) {
        maxHP -= damage;
    }
}
