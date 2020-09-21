using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class AddForces : MonoBehaviour
{
    public Vector3 forceVector;
    private PhysicsEngine _physicsEngine;

    private void Start()
    {
        _physicsEngine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        _physicsEngine.AddForce(forceVector);
    }
}
