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
/// Upgrades traction strength of the car controller.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Customization/RCCP Vehicle Upgrade Handling")]
public class RCCP_VehicleUpgrade_Handling : RCCP_Component {

    private int _handlingLevel = 0;
    public int HandlingLevel {
        get {
            return _handlingLevel;
        }
        set {
            if (value <= 5)
                _handlingLevel = value;
        }
    }

    [HideInInspector] public float defHandling = 0f;
    [Range(.1f, .6f)] public float maxUpgradedHandlingStrength = .25f;

    /// <summary>
    /// Updates handling and initializes it.
    /// </summary>
    public void Initialize() {

        if (!CarController.Stability) {

            Debug.LogError("Stability component couldn't found in the vehicle. RCCP_VehicleUpgrade_Handling needs it to upgrade handling.");
            enabled = false;
            return;

        }

        CarController.Stability.tractionHelperStrength = Mathf.Lerp(defHandling, maxUpgradedHandlingStrength, HandlingLevel / 5f);

    }

    /// <summary>
    /// Updates handling strength and save it.
    /// </summary>
    public void UpdateStats() {

        if (!CarController.Stability) {

            Debug.LogError("Stability component couldn't found in the vehicle. RCCP_VehicleUpgrade_Handling needs it to upgrade handling.");
            enabled = false;
            return;

        }

        CarController.Stability.tractionHelperStrength = Mathf.Lerp(defHandling, maxUpgradedHandlingStrength, HandlingLevel / 5f);

    }

    private void Update() {

        if (!CarController.Stability) {

            Debug.LogError("Stability component couldn't found in the vehicle. RCCP_VehicleUpgrade_Handling needs it to upgrade handling.");
            enabled = false;
            return;

        }

        //  Make sure max handling is not smaller.
        if (maxUpgradedHandlingStrength < CarController.Stability.tractionHelperStrength)
            maxUpgradedHandlingStrength = CarController.Stability.tractionHelperStrength;

    }

    private void Reset() {

        if (!GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Stability>(true)) {

            Debug.LogError("Stability component couldn't found in the vehicle. RCCP_VehicleUpgrade_Handling needs it to upgrade handling.");
            enabled = false;
            return;

        }

        maxUpgradedHandlingStrength = GetComponentInParent<RCCP_CarController>(true).GetComponentInChildren<RCCP_Stability>(true).tractionHelperStrength + .2f;

    }

}
