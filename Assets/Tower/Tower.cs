using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Tooltip("How long it takes to build each stage of the tower, there are 2 stages.")] 
    [SerializeField] private float buildStageDelay = 1f;
    [Tooltip("The cost to build 1 tower.")]
    [SerializeField] private int towerCost = 50;

    private void Start()
    {
        StartCoroutine(Build());
    }

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

    private IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandchild in child) {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);

            yield return new WaitForSeconds(buildStageDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

}
