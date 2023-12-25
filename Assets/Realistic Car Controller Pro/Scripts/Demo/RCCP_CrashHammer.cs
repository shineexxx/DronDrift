﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2023 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Operates the hammer in the damage demo scene.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RCCP_CrashHammer : MonoBehaviour {

    public Transform hingePoint;        //  Hinge point of the joint.
    private Rigidbody rigid;        //  Rigidbody.
    public Vector3 torque;      //  Torque.

    public float length = 1f;       //  Length of the wave.
    public float speed = 1f;        //  Speed of the wave.

    private void Start() {

        //  Getting rigidbody.
        rigid = GetComponent<Rigidbody>();

        //  Creating hinge with configurable joint.
        CreateHinge();

    }

    private void FixedUpdate() {

        //  If no rigid, return.
        if (!rigid)
            return;

        //  Apply force.
        rigid.AddRelativeForce(torque * ((float)Mathf.Sin(Time.time * speed) * length), ForceMode.Acceleration);

    }

    /// <summary>
    /// Creates hinge with configurable joint.
    /// </summary>
    private void CreateHinge() {

        GameObject hinge = new GameObject("Hinge_" + transform.name);
        hinge.transform.SetPositionAndRotation(hingePoint.position, hingePoint.rotation);

        Rigidbody hingeRigid = hinge.AddComponent<Rigidbody>();
        hingeRigid.isKinematic = true;
        hingeRigid.useGravity = false;

        AttachHinge(hingeRigid);

    }

    /// <summary>
    /// Sets connected body of the configurable joint.
    /// </summary>
    /// <param name="hingeRigid"></param>
    private void AttachHinge(Rigidbody hingeRigid) {

        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();

        if (!joint) {

            print("Configurable Joint of the " + transform.name + " not found! Be sure this gameobject has Configurable Joint with right config.");
            enabled = false;
            return;

        }

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = hingeRigid;
        joint.connectedAnchor = Vector3.zero;

    }

    private void Reset() {

        if (hingePoint == null) {

            hingePoint = new GameObject("Hinge Point").transform;
            hingePoint.SetParent(transform, false);

        }

    }

}