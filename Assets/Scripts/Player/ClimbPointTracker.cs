using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClimbDirection
{
    Left, Right
}

public class ClimbPointTracker : MonoBehaviour
{
    private enum TrackerDirection
    {
        Head, Feet
    }

    [SerializeField]
    private TrackerDirection trackerDirection;

    private List<Collider2D> availableClimbPoints;

    private void Start()
    {
        availableClimbPoints = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        availableClimbPoints.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        availableClimbPoints.Remove(collision);
    }

    public bool ClimbAvailable(ClimbDirection dir, out Vector2 climbPosition)
    {
        foreach (var point in availableClimbPoints)
        {
            if ((dir == ClimbDirection.Left && point.transform.localScale.x < 0.0f) || 
                (dir == ClimbDirection.Right && point.transform.localScale.x >= 0.0f))
            {
                climbPosition = new Vector2(point.transform.position.x, point.transform.position.y);
                return true;
            }
        }
        climbPosition = Vector2.zero;
        return false;
    }
}
