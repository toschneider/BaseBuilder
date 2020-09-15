using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    private void Start()
    {

        //PathRequestManager.RequestPath(new PathRequest(StartNode, EndNode, OnPathFound));

    }

    public void OnPathFound(List<Waypoint> waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            List<Waypoint> path = new List<Waypoint>(waypoints);
            LineRenderer Line = GetComponent<LineRenderer>();
            Line.transform.position = this.transform.position;
            Line.positionCount = path.Count;

            for (int i = 0; i < path.Count; ++i)
            {
                Line.SetPosition(i, new Vector3(path[i].X, path[i].Y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
