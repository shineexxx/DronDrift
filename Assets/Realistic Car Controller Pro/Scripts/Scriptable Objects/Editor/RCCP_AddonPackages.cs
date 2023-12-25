﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2023 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All addon packages.
/// </summary>
public class RCCP_AddonPackages : ScriptableObject {

    #region singleton
    private static RCCP_AddonPackages instance;
    public static RCCP_AddonPackages Instance { get { if (instance == null) instance = Resources.Load("RCCP_AddonPackages") as RCCP_AddonPackages; return instance; } }
    #endregion

    public Object BCGSharedAssets;
    public Object PhotonPUN2;
    public Object ProFlare;

    public string GetAssetPath(Object pathObject) {

        string path = UnityEditor.AssetDatabase.GetAssetPath(pathObject);
        return path;

    }

}
