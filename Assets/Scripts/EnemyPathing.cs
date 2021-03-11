using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed = 2f;
    int waypointIndex = 0;
    [SerializeField] private bool loopPath = false;

    // Use this for initialization
    void Start () {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig newWaveConfig)
    {
        waveConfig = newWaveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else if (loopPath)
        {
            waypointIndex = 0;
        } else 
        {
            Destroy(gameObject);
        }
    }
}