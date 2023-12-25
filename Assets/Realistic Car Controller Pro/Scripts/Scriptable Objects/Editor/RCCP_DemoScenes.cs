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
/// All demo scenes.
/// </summary>
public class RCCP_DemoScenes : ScriptableObject {

    #region singleton
    private static RCCP_DemoScenes instance;
    public static RCCP_DemoScenes Instance { get { if (instance == null) instance = Resources.Load("RCCP_DemoScenes") as RCCP_DemoScenes; return instance; } }
    #endregion

    public Object city_AIO;
    public Object demo_City;
    public Object demo_CarSelection;
    public Object demo_APIBlank;
    public Object demo_BlankMobile;
    public Object demo_Damage;
    public Object demo_Customization;
    public Object demo_OverrideInputs;
    public Object demo_Transport;

#if BCG_ENTEREXIT
    public Object demo_CityFPS;
    public Object demo_CityTPS;
#endif

#if RCCP_PHOTON
    public Object demo_PUN2City;
    public Object demo_PUN2Lobby;
#endif

#if BCG_RTRC
    public Object demo_City_Traffic;
#endif

    public string GetAssetPath(Object pathObject) {

        string path = UnityEditor.AssetDatabase.GetAssetPath(pathObject);
        return path;

    }

}
