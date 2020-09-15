using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public void FindPath(PathRequest request, Action<PathResult> callback)
    {

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];

        Waypoint startNode = request.pathStart;
        Waypoint targetNode = request.pathEnd;

        //List<Waypoint> shortestPath = Dijsktra(startNode, targetNode);

        //callback(new PathResult(shortestPath, true, request.callback));

    }

}
