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
/// Transmits the received power from the engine --> clutch --> gearbox to the axle. 
/// Open differential = RPM difference between both wheels will decide to which wheel needs more traction or not. 
/// Limited = almost same with open with slip limitation. Higher percents = more close to the locked system. 
/// Locked = both wheels will have the same traction.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Drivetrain/RCCP Differential")]
public class RCCP_Differential : RCCP_Component {

    public bool overrideDifferential = false;

    public enum DifferentialType {

        Open,
        Limited,
        FullLocked,
        Direct

    }

    public DifferentialType differentialType = DifferentialType.Limited;       // Differential type.

    [Range(50f, 100f)] public float limitedSlipRatio = 80f;     //  LSD.

    public float finalDriveRatio = 3.73f;       //  Final drive ratio multiplier. Faster accelerations and lower top speeds on higher values.
    public float receivedTorqueAsNM = 0f;       //  Received torque from the component. It should be the gearbox in this case.
    public float producedTorqueAsNM = 0f;       //  Deliveted torque to the component. It should be an axle in this case.

    public float leftWheelRPM = 0f;     //  Left wheel rpm.
    public float rightWheelRPM = 0f;        //  Right wheel rpm.

    public float wheelSlipRatio = 0f;       //  Wheel slip ratio.
    public float leftWheelSlipRatio = 0f;       //  Left slip ratio.
    public float rightWheelSlipRatio = 0f;      //  Right slip ratio.

    public float outputLeft = 0f;       //  Output of the left wheel.
    public float outputRight = 0f;      //  Output of the right wheel.

    public RCCP_Axle connectedAxle;     //  Connected axle to this differential. Each differential must have an axle.

    private void FixedUpdate() {

        //  Return if overriding the differential. This means an external class is adjusting differential inputs.
        if (overrideDifferential)
            return;

        //  Return if not connected to any axle.
        if (!connectedAxle)
            return;

        Gears();
        Output();

    }

    /// <summary>
    /// Calculating output torque of the left and right side.
    /// </summary>
    private void Gears() {

        //  Get rpm.
        if (connectedAxle.leftWheelCollider && connectedAxle.leftWheelCollider.isActiveAndEnabled)
            leftWheelRPM = Mathf.Abs(connectedAxle.leftWheelCollider.WheelCollider.rpm);
        else
            leftWheelRPM = 0f;

        if (connectedAxle.rightWheelCollider && connectedAxle.rightWheelCollider.isActiveAndEnabled)
            rightWheelRPM = Mathf.Abs(connectedAxle.rightWheelCollider.WheelCollider.rpm);
        else
            rightWheelRPM = 0f;

        //  Sum rpm and difference.
        float sumRPM = leftWheelRPM + rightWheelRPM;
        float diffRPM = leftWheelRPM - rightWheelRPM;

        //  Calculating the wheel slip ratio between left and right wheel.
        wheelSlipRatio = Mathf.InverseLerp(0f, sumRPM, Mathf.Abs(diffRPM));

        switch (differentialType) {

            //  If differential type is open...
            case DifferentialType.Open:

                if (Mathf.Sign(diffRPM) == 1) {

                    leftWheelSlipRatio = wheelSlipRatio;
                    rightWheelSlipRatio = -wheelSlipRatio;

                } else {

                    leftWheelSlipRatio = -wheelSlipRatio;
                    rightWheelSlipRatio = wheelSlipRatio;

                }

                break;

            //  If differential type is LSD...
            case DifferentialType.Limited:

                wheelSlipRatio *= Mathf.Lerp(1f, 0f, (limitedSlipRatio / 100f));

                if (Mathf.Sign(diffRPM) == -1) {

                    leftWheelSlipRatio = -wheelSlipRatio;
                    rightWheelSlipRatio = wheelSlipRatio;

                } else {

                    leftWheelSlipRatio = wheelSlipRatio;
                    rightWheelSlipRatio = -wheelSlipRatio;

                }

                break;

            //  If differential type is full locked...
            case DifferentialType.FullLocked:

                if (Mathf.Sign(diffRPM) == -1) {

                    leftWheelSlipRatio = -.5f;
                    rightWheelSlipRatio = .5f;

                } else {

                    leftWheelSlipRatio = .5f;
                    rightWheelSlipRatio = -.5f;

                }

                break;

            case DifferentialType.Direct:

                leftWheelSlipRatio = 0f;
                rightWheelSlipRatio = 0f;

                break;

        }

    }

    /// <summary>
    /// Overrides the differential output with given values.
    /// </summary>
    /// <param name="targetOutputLeft"></param>
    /// <param name="targetOutputRight"></param>
    public void OverrideDifferential(float targetOutputLeft, float targetOutputRight) {

        outputLeft = targetOutputLeft;
        outputRight = targetOutputRight;
        producedTorqueAsNM = outputLeft + outputRight;

        connectedAxle.isPower = true;
        connectedAxle.ReceiveOutput(targetOutputLeft, targetOutputRight);

    }

    /// <summary>
    /// Receive torque from the component.
    /// </summary>
    /// <param name="output"></param>
    public void ReceiveOutput(RCCP_Output output) {

        if (overrideDifferential)
            return;

        receivedTorqueAsNM = output.NM;

    }

    /// <summary>
    /// Output to the left and right wheels.
    /// </summary>
    private void Output() {

        producedTorqueAsNM = receivedTorqueAsNM * finalDriveRatio;

        outputLeft = producedTorqueAsNM / 2f;
        outputRight = producedTorqueAsNM / 2f;

        outputLeft -= producedTorqueAsNM * leftWheelSlipRatio;
        outputRight -= producedTorqueAsNM * rightWheelSlipRatio;

        connectedAxle.isPower = true;
        connectedAxle.ReceiveOutput(outputLeft, outputRight);

    }

    public void Reload() {

        leftWheelRPM = 0f;
        rightWheelRPM = 0f;
        wheelSlipRatio = 0f;
        leftWheelSlipRatio = 0f;
        rightWheelSlipRatio = 0f;
        outputLeft = 0f;
        outputRight = 0f;
        producedTorqueAsNM = 0f;

    }

}
