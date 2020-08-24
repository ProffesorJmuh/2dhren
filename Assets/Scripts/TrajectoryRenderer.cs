using System;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : ITrajectoryRenderer
{
    private LineRenderer lineRendererComponent;

    private List<GameObject> FindGameObjectsWithLayer(int layer){
        var goArray = FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
         for (var i = 0; i<goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                 goList.Add(goArray[i]);
             }
         }
         if (goList.Count == 0) {
             return null;
         }
         return goList;
    }

    private List<GameObject> hitableObjects = new List<GameObject>();
    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();

        hitableObjects = FindGameObjectsWithLayer(LayerMask.NameToLayer("Ground"));
        
    }

    public override void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        lineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;


            if (points[i].y < -5)
            {
                lineRendererComponent.positionCount = i + 1;
                break;
            }
        }

        
        
        lineRendererComponent.SetPositions(points);
    }
}