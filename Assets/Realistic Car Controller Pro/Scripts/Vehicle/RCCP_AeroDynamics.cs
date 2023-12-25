//----------------------------------------------
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
/// Manages the dynamics of the vehicle.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Addons/RCCP Dynamics")]
public class RCCP_AeroDynamics : RCCP_Component {

    public Transform COM;       //  Centre of mass.
    [Range(-100f, 100f)] public float downForce = 10f;      //  Downforce will be applied to the vehicle with speed related. Vehicle will be more controllable on higher speeds.
    [Range(0f, 100f)] public float airResistance = 10f;      //  Downforce will be applied to the vehicle with speed related. Vehicle will be more controllable on higher speeds.

    public bool dynamicCOM = false;     //  Sets COM position in te update method. Enable it if you are going to change COM position at runtime.
    public bool autoReset = true;       //  Resets the vehicle if upside down.
    public float autoResetTime = 3f;        //  Timer for reset.
    private float autoResetTimer = 0f;

    public override void Start() {

        base.Start();

        //  Assigning center of mass position.
        CarController.Rigid.centerOfMass = transform.InverseTransformPoint(COM.position);

    }

    private void FixedUpdate() {

        //  Assigning center of mass position.
        if (dynamicCOM)
            CarController.Rigid.centerOfMass = transform.InverseTransformPoint(COM.position);

        //  Applying downforce to the vehicle.
        CarController.Rigid.AddRelativeForce(Vector3.down * downForce * Mathf.Abs(CarController.speed), ForceMode.Force);

        float correctedForce = airResistance * Mathf.Lerp(0f, 50f, Mathf.Abs(CarController.speed / 35f));
        correctedForce *= Mathf.Lerp(1f, .1f, Mathf.Abs(CarController.speed) / 240f);

        //  Applying air drag resistance to the vehicle.
        CarController.Rigid.AddRelativeForce(-Vector3.forward * correctedForce * CarController.direction, ForceMode.Force);

        //  If auto-reset is enabled, check upside down.
        if (autoReset)
            CheckUpsideDown();

    }

    /// <summary>
    /// Resets the car if upside down.
    /// </summary>
    private void CheckUpsideDown() {

        //  If vehicle speed is below 5 and upside down, it will count to the target seconds and resets the vehicle.
        if (Mathf.Abs(CarController.speed) < 5 && !CarController.Rigid.isKinematic) {

            if (CarController.transform.eulerAngles.z < 300 && CarController.transform.eulerAngles.z > 60) {

                autoResetTimer += Time.deltaTime;

                if (autoResetTimer > autoResetTime) {

                    CarController.transform.SetPositionAndRotation(

                        new Vector3(CarController.transform.position.x, CarController.transform.position.y + 3, CarController.transform.position.z),
                        Quaternion.Euler(0f, CarController.transform.eulerAngles.y, 0f)

                        );

                    autoResetTimer = 0f;

                }

            }

        }

    }

    public void Reload() {

        autoResetTimer = 0f;

    }

    private void Reset() {

        if (transform.Find("COM"))
            DestroyImmediate(transform.Find("COM").gameObject);

        GameObject newCom = new GameObject("COM");
        newCom.transform.SetParent(transform, false);
        COM = newCom.transform;
        COM.transform.localPosition = new Vector3(0f, -.25f, 0f);

    }

}
