using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardCon : MonoBehaviour
{
    public Transform Path;//set guard route
    public float speed = 2f;
    public float wait = 0.2f;
    public float Rotationspeed = 10f;
    void Start()
    {
        Vector3[] waypoints = new Vector3[Path.childCount];//current only set 4 waypoints,we can easy set more later
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = Path.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);//adjust guard position 

        }
        StartCoroutine(route(waypoints));
    }
    IEnumerator route(Vector3[] waypoints)
    {
        transform.position = waypoints[1];
        int nextwaypointIndex = 2;
        Vector3 nextwaypoint = waypoints[nextwaypointIndex];
        transform.LookAt(nextwaypoint);//guard face to next waypoint
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextwaypoint, speed * Time.deltaTime);//guard move speed and route
            if (transform.position == nextwaypoint)
            {
                nextwaypointIndex = (nextwaypointIndex + 1) % waypoints.Length;
                nextwaypoint = waypoints[nextwaypointIndex];
                yield return new WaitForSeconds(wait);
                yield return StartCoroutine(guardrotation(nextwaypoint));
            }
            yield return null;
        }
    }
    IEnumerator guardrotation(Vector3 rotation)
    {
        Vector3 GoNextWaypoint = (rotation - transform.position);
        float guardangle = 90 - Mathf.Atan2(GoNextWaypoint.z, GoNextWaypoint.x) * Mathf.Rad2Deg;
        while (Mathf.DeltaAngle(transform.eulerAngles.y, guardangle) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, guardangle, Rotationspeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }




    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 StartPosition = Path.GetChild(0).position;//get waypoint1 and ste it as start position
        Vector3 previousPosition = StartPosition;
        foreach (Transform waypoint in Path)
        {
            Gizmos.DrawSphere(waypoint.position, 0.4f);//show the every guard's route
            Gizmos.DrawLine(previousPosition, waypoint.position);//current position to next position route
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, StartPosition);//guard back the start position,so guard's route is a loop.
    }
    // Update is called once per frame

}

