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
/// UI siren button.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/UI/Modification/RCCP UI Siren Button")]
public class RCCP_UI_Siren : MonoBehaviour {

    public int index = 0;

    public void Upgrade() {

        //  Finding the player vehicle.
        RCCP_CarController playerVehicle = RCCP_SceneManager.Instance.activePlayerVehicle;

        //  If no player vehicle found, return.
        if (!playerVehicle)
            return;

        //  If player vehicle doesn't have the customizer component, return.
        if (!playerVehicle.Customizer)
            return;

        if (!playerVehicle.Customizer.PaintManager)
            return;

        playerVehicle.Customizer.SirenManager.Upgrade(index);

    }

}
