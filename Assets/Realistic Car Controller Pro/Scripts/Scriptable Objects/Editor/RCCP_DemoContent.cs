//----------------------------------------------
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
/// All removable demo contents.
/// </summary>
public class RCCP_DemoContent : ScriptableObject {

    #region singleton
    private static RCCP_DemoContent instance;
    public static RCCP_DemoContent Instance { get { if (instance == null) instance = Resources.Load("RCCP_DemoContent") as RCCP_DemoContent; return instance; } }
    #endregion

    public Object[] contents;

    public Object builtinShaderPackage;
    public Object URPShaderPackage;

    public Object builtinShadersContent;
    public Object URPShadersContent;

    public string GetAssetPath(Object pathObject) {

        string path = UnityEditor.AssetDatabase.GetAssetPath(pathObject);
        return path;

    }

}
