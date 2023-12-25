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
/// All demo materials.
/// </summary>
public class RCCP_DemoMaterials : ScriptableObject {

    #region singleton
    private static RCCP_DemoMaterials instance;
    public static RCCP_DemoMaterials Instance { get { if (instance == null) instance = Resources.Load("RCCP_DemoMaterials") as RCCP_DemoMaterials; return instance; } }
    #endregion

    [System.Serializable]
    public class MaterialStructure {

        public Material material;

        public string defaultShader;
        public string DefaultShader {

            get {

                if (defaultShader == "" && material)
                    defaultShader = material.shader.name;

                return defaultShader;

            }set {
                defaultShader = value;
            }

        }
    }

    public MaterialStructure[] demoMaterials;

    //public Material[] demoMaterials;
    public Material[] vehicleBodyMaterials;

}
