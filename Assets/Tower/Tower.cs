using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost = 50;

    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null || bank.CurrentBalance < towerCost)
        {
            return false;
        }

        bank.Withdraw(towerCost);
        Instantiate(towerPrefab, position, Quaternion.identity);
        return true;
    }
}
