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
/// Manager for upgradable spoilers.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Customization/RCCP Vehicle Upgrade Spoiler Manager")]
public class RCCP_VehicleUpgrade_SpoilerManager : RCCP_UpgradeComponent, IRCCP_UpgradeComponent {

    public RCCP_VehicleUpgrade_Spoiler[] spoilers;        //  All upgradable spoilers.

    public int spoilerIndex = -1;     //  Last selected spoiler index.
    public bool paintSpoilers = true;       //  Painting the spoilers?

    public void Initialize() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(false);

        //  Getting index of the loadouts spoiler.
        spoilerIndex = Loadout.spoiler;

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        spoilers[spoilerIndex].gameObject.SetActive(true);

        //  Getting saved color of the spoiler.
        if (Loadout.paint != new Color(1f, 1f, 1f, 0f))
            Paint(Loadout.paint);

    }

    public void DisableAll() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(false);

    }

    public void EnableAll() {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return
        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(true);

    }

    /// <summary>
    /// Unlocks target spoiler index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void Upgrade(int index) {

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Index of the spoiler.
        spoilerIndex = index;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(false);

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        spoilers[spoilerIndex].gameObject.SetActive(true);

        if (Loadout.paint != new Color(1f, 1f, 1f, 0f) && spoilers[spoilerIndex].bodyRenderer != null)
            Paint(Loadout.paint);

        //  Refreshing the loadout.
        Refresh(this);

        //  Saving the loadout.
        if (CarController.Customizer.autoSave)
            Save();

    }

    /// <summary>
    /// Unlocks target spoiler index and saves it.
    /// </summary>
    /// <param name="index"></param>
    public void UpgradeWithoutSave(int index) {

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Index of the spoiler.
        spoilerIndex = index;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(false);

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  If index is not -1, enable the corresponding spoiler.
        spoilers[spoilerIndex].gameObject.SetActive(true);

        if (Loadout.paint != new Color(1f, 1f, 1f, 0f) && spoilers[spoilerIndex].bodyRenderer != null)
            Paint(Loadout.paint);

    }

    /// <summary>
    /// Painting.
    /// </summary>
    /// <param name="newColor"></param>
    public void Paint(Color newColor) {

        //  If spoilers is null, return.
        if (spoilers == null)
            return;

        //  If spoilers is null, return.
        if (spoilers.Length < 1)
            return;

        //  If spoiler index is -1, return.
        if (spoilerIndex == -1)
            return;

        //  Painting all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].UpdatePaint(newColor);

    }

    /// <summary>
    /// Restores the settings to default.
    /// </summary>
    public void Restore() {

        spoilerIndex = -1;

        //  If sirens is null, return.
        if (spoilers == null)
            return;

        if (spoilers.Length < 1)
            return;

        //  Disabling all spoilers.
        for (int i = 0; i < spoilers.Length; i++)
            spoilers[i].gameObject.SetActive(false);

    }

}
