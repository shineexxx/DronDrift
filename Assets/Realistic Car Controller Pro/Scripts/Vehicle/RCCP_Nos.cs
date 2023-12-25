//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright � 2014 - 2023 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// NOS / Boost used to multiply engine torque for a limited time.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Other Addons/RCCP Nos")]
public class RCCP_Nos : RCCP_Component {

    [HideInInspector] public bool nosInUse = false;     //  Nos is currently in use now?

    public float torqueMultiplier = 1.5f;       //  Engine torque multiplier.

    [Range(0f, 1f)] public float amount = 1f;       //  Amount of nos in range between 0 - 1.
    public float durationTime = 3f;     //  Maximum duration of the nos in seconds.
    private float timer = 3f;       //  Current timer.

    public float regenerateTime = 2f;       //  Regenerates the nos after this seconds.
    private float regenerateTimer = 1f;     //  Current timer to regenerate.
    public float regenerateRate = 1f;       //  Regenerate rate. Nos will be filled up more faster on higher values.

    private void Update() {

        //  If no engine found, return.
        if (!CarController.Engine)
            return;

        //  If nos input is above 0.5, enable nos. Otherwise disable.
        nosInUse = CarController.nosInput_P >= .5f ? true : false;

        //  If engine is not running, set it to off.
        if (!CarController.Engine.engineRunning)
            nosInUse = false;

        //  If throttle input is not high enough, set it to off.
        if (CarController.throttleInput_V < .5f)
            nosInUse = false;

        //  If timer is not enough, set it to off.
        if (timer <= .1f)
            nosInUse = false;

        //  If nos is currently in use now, decrease the timer and multiply the enngine torque.
        if (nosInUse) {

            regenerateTimer = 0f;

            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, Mathf.Infinity);

            CarController.Engine.Multiply(torqueMultiplier);

        }

        //  Regenerating the nos with timer.
        if (regenerateTimer < regenerateTime)
            regenerateTimer += Time.deltaTime;

        if (regenerateTimer >= regenerateTime)
            timer += Time.deltaTime * regenerateRate;

        timer = Mathf.Clamp(timer, 0f, durationTime);
        amount = timer / durationTime;

    }

    public void Reload() {

        //  Make sure nos in use is disabled when enabling / disabling the vehicle.
        nosInUse = false;

    }

}
