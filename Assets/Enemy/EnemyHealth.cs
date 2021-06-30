using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] [Min(1)] private int maxHP = 5;
    [Tooltip("Adds given value to enemy maxHealth when they die so when they respawn they'll be harder to kill.")]
    [SerializeField] private int difficultyRamp = 1;

    private int currentHP;
    private Enemy enemy;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
            maxHP += difficultyRamp;
        }
    }
}
