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
/// Animation attached to "Animation Pivot" of the Cinematic Camera is feeding FOV float value.
/// </summary>
public class RCCP_FOVForCinematicCamera : MonoBehaviour {

    private RCCP_CinematicCamera cinematicCamera;        //  Cinematic camera.
    public float FOV = 30f;     //  Target field of view.

    private void Awake() {

        //  Getting cinematic camera.
        cinematicCamera = GetComponentInParent<RCCP_CinematicCamera>(true);

    }

    private void Update() {

        if (!cinematicCamera)
            return;

        //  Setting field of view of the cinematic camera.
        cinematicCamera.targetFOV = FOV;

    }

}
