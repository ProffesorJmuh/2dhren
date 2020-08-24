using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRendererAdvanced : ITrajectoryRenderer
{
    public GameObject BulletPrefab;

    private LineRenderer lineRendererComponent;
    private Dictionary<Rigidbody2D, BodyData> savedBodies = new Dictionary<Rigidbody2D, BodyData>();

    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();

        foreach (var rb in FindObjectsOfType<Rigidbody2D>())
        {
            savedBodies.Add(rb, new BodyData());
        }
    }

    public void AddBody(Rigidbody2D rb)
    {
        savedBodies.Add(rb, new BodyData());
    }

    public override void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        // Подготовка:
        foreach (var body in savedBodies)
        {
            if(body.Value != null && body.Key != null)
            {
                body.Value.position = body.Key.transform.position;
                body.Value.rotation = body.Key.transform.rotation;
                body.Value.velocity = body.Key.velocity;
                body.Value.angularVelocity = body.Key.angularVelocity;
            }
        }

        GameObject bullet = Instantiate(BulletPrefab, origin, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(speed, ForceMode2D.Impulse);

        Physics2D.autoSimulation = false;

        // Симуляция:
        Vector3[] points = new Vector3[50];

        points[0] = origin;
        for (int i = 1; i < points.Length; i++)
        {
            Physics2D.Simulate(0.2f);

            points[i] = bullet.transform.position;
        }

        lineRendererComponent.SetPositions(points);

        // Зачистка:
        Physics2D.autoSimulation = true;

        foreach (var body in savedBodies)
        {
            if (body.Value != null && body.Key != null)
            {
                body.Key.transform.position = body.Value.position;
                body.Key.transform.rotation = body.Value.rotation;
                body.Key.velocity = body.Value.velocity;
                body.Key.angularVelocity = body.Value.angularVelocity;
            }
        }

        Destroy(bullet.gameObject);
    }
}

public class BodyData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public float angularVelocity;
}