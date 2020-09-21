
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PhysicsEngine : MonoBehaviour
{
    [Header("Vectors")]
    public Vector3 velocityVector;
    public Vector3 netForceVector;
    public Vector3 accelerationVector;
    
    private List<Vector3> forceVectorList = new List<Vector3>();
    [Header("Variables")] public float mass;
    public bool showTrails = true;
    private LineRenderer lineRenderer;
    private int numberOfForces;

    private void Start()
    {
        LineRendererData();
    }

    private void Update()
    {
        DrawLine();
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
        SumForces();
        //Update position
        
    }

    public void AddForce(Vector3 forceVector3)
    {
        forceVectorList.Add(forceVector3);
    }

    void SumForces()
    {
        {
            netForceVector = Vector3.zero;

            foreach (Vector3 forceVector3 in forceVectorList)
            {
                netForceVector += forceVector3;
            }

            forceVectorList = new List<Vector3>(); // Clear the list
        }
    }
    
    void UpdateVelocity()
    {
        accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;
    }

    //Line Renderer methods to draw forces
    void LineRendererData()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.startWidth = 0.2F;
        lineRenderer.useWorldSpace = false;
    }

    void DrawLine()
    {
        if (showTrails) {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.positionCount = numberOfForces * 2;
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList) {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i+1, -forceVector);
                i = i + 2;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}