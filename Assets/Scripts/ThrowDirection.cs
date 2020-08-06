using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDirection : MonoBehaviour
{
    // public GameObject BulletPrefab;
    public float Power = 100;

    public TrajectoryRenderer Trajectory;
    //public TrajectoryRendererAdvanced Trajectory;

    private Camera mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        float enter;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        // transform.rotation = Quaternion.LookRotation(speed);
        Trajectory.ShowTrajectory(transform.position, speed);
/*
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce(speed, ForceMode.VelocityChange);
            //Trajectory.AddBody(bullet);
        }*/
    }
}

