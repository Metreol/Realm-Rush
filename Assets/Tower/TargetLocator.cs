using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private float range = 15;
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectiles;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        // IF no target assigned OR target out of range OR target is not active (destroyed) THEN FindNewTarget ELSE target is valid so take aim and fire!
        if (target == null || Vector3.Distance(transform.position, target.position) > range || !target.gameObject.activeInHierarchy)
        {
            FindNewTarget();
        }
        else
        {
            weapon.LookAt(target);
            Attack(true);
        }
    }

    // When current target is not valid look for a new target. 
    private void FindNewTarget()
    {
        Attack(false);

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float distToClosestTarget = Mathf.Infinity;

        foreach (Enemy nextTarget in enemies)
        {
            if (nextTarget.gameObject.activeInHierarchy)
            {
                float distToNextTarget = Vector3.Distance(transform.position, nextTarget.transform.position);

                if (distToNextTarget < distToClosestTarget)
                {
                    closestTarget = nextTarget.transform;
                    distToClosestTarget = distToNextTarget;
                }
            }
        }

        target = closestTarget;
    }

    private void Attack(bool isEnabled)
    {
        var emissionModule = projectiles.emission;
        emissionModule.enabled = isEnabled;
    }
}
