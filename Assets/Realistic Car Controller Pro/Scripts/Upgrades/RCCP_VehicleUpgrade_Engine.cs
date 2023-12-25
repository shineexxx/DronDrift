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
/// Upgrades engine of the car controller.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Customization/RCCP Vehicle Upgrade Engine")]
public class RCCP_VehicleUpgrade_Engine : RCCP_Component {

    private int _engineLevel = 0;
    public int EngineLevel {
        get {
            return _engineLevel;
        }
        set {
            if (value <= 5)
                _engineLevel = value;
        }
    }

    [HideInInspector] public float defEngine = 0f;
    [Range(200, 1000)] public float maxUpgradedEngineTorque = 500f;

    /// <summary>
    /// Updates engine torque and initializes it.
    /// </summary>
    public void Initialize() {

        if (!CarController.Engine) {

            Debug.LogError("Engine couldn't found in the vehicle. RCCP_VehicleUpgrade_Engine needs it to upgrade the engine level");
            enabled = false;
            return;

        }

        CarController.Engine.maximumTorqueAsNM = Mathf.Lerp(defEngine, maxUpgradedEngineTorque, EngineLevel / 5f);

    }

    /// <summary>
    /// Updates engine torque and save it.
    /// </summary>
    public void UpdateStats() {

        if (!CarController.Engine) {

            Debug.LogError("Engine couldn't found in the vehicle. RCCP_VehicleUpgrade_Engine needs it to upgrade the engine level");
            enabled = false;
            return;

        }

        CarController.Engine.maximumTorqueAsNM = Mathf.Lerp(defEngine, maxUpgradedEngineTorque, EngineLevel / 5f);

    }

    private void Update() {

        if (!CarController.Engine) {

            Debug.LogError("Engine couldn't found in the vehicle. RCCP_VehicleUpgrade_Engine needs it to upgrade the engine level");
            enabled = false;
            return;

        }

        //  Make sure max torque is not smaller.
        if (maxUpgradedEngineTorque < CarController.Engine.maximumTorqueAsNM)
            maxUpgradedEngineTorque = CarController.Engine.maximumTorqueAsNM;

    }

    private void Reset() {

        if (!GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Engine>(true)) {

            Debug.LogError("Engine couldn't found in the vehicle. RCCP_VehicleUpgrade_Engine needs it to upgrade the engine level");
            enabled = false;
            return;

        }

        maxUpgradedEngineTorque = GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Engine>(true).maximumTorqueAsNM + 150f;

    }

}
