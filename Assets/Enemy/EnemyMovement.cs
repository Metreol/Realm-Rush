using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float speed = 1f;
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();
    
    private bool started;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        started = false;
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();
        
        Waypoint[] waypoints = GameObject.FindGameObjectWithTag("Path") // Gets the object with tag "Path" which contains all the path waypoints...
            .GetComponentsInChildren<Waypoint>(); // So we get the children of this object...

        path.AddRange(waypoints); // And add them to our path List.
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    // This is an example of a coroutine, hence being called with StartCoroutine above. 
    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            if (!started)
            {
                started = true;
                ReturnToStart();
            }
            else
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = waypoint.transform.position;
                float travelPercent = 0f;

                transform.LookAt(endPosition);

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        FinishPath();
    }

}
