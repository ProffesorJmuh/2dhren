using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITrajectoryRenderer: MonoBehaviour
{
    public abstract void ShowTrajectory(Vector3 origin, Vector3 speed);
}
