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
/// Upgrades brake torque of the car controller.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Customization/RCCP Vehicle Upgrade Brake")]
public class RCCP_VehicleUpgrade_Brake : RCCP_Component {

    private int _brakeLevel = 0;
    public int BrakeLevel {
        get {
            return _brakeLevel;
        }
        set {
            if (value <= 5)
                _brakeLevel = value;
        }
    }

    [HideInInspector] public float defBrake = 0f;
    [Range(2000, 10000)] public float maxUpgradedBrakeTorque = 6000f;

    /// <summary>
    /// Updates brake torque and initializes it.
    /// </summary>
    public void Initialize() {

        if (CarController.AxleManager.Axles == null || CarController.AxleManager.Axles != null && CarController.AxleManager.Axles.Count < 1) {

            Debug.LogError("Axles couldn't found in your vehicle. RCCP_VehicleUpgrade_Brake needs it to upgrade the brake level.");
            enabled = false;
            return;

        }

        for (int i = 0; i < CarController.AxleManager.Axles.Count; i++)
            CarController.AxleManager.Axles[i].maxBrakeTorque = Mathf.Lerp(defBrake, maxUpgradedBrakeTorque, BrakeLevel / 5f);

    }

    /// <summary>
    /// Updates brake torque and save it.
    /// </summary>
    public void UpdateStats() {

        if (CarController.AxleManager.Axles == null || CarController.AxleManager.Axles != null && CarController.AxleManager.Axles.Count < 1) {

            Debug.LogError("Axles couldn't found in your vehicle. RCCP_VehicleUpgrade_Brake needs it to upgrade the brake level.");
            enabled = false;
            return;

        }

        for (int i = 0; i < CarController.AxleManager.Axles.Count; i++)
            CarController.AxleManager.Axles[i].maxBrakeTorque = Mathf.Lerp(defBrake, maxUpgradedBrakeTorque, BrakeLevel / 5f);

    }

    private void Update() {

        //  Make sure max brake is not smaller.
        if (maxUpgradedBrakeTorque < 0)
            maxUpgradedBrakeTorque = 0;

    }

    private void Reset() {

        if (!GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Axles>(true)) {

            Debug.LogError("Axles couldn't found in your vehicle. RCCP_VehicleUpgrade_Brake needs it to upgrade the brake level.");
            enabled = false;
            return;

        }

        float averageBrakeforce = 0f;

        for (int i = 0; i < GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Axles>(true).Axles.Count; i++)
            averageBrakeforce += GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Axles>(true).Axles[i].maxBrakeTorque;

        averageBrakeforce /= GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Axles>(true).Axles.Count;

        maxUpgradedBrakeTorque = averageBrakeforce + 800f;

    }

}
