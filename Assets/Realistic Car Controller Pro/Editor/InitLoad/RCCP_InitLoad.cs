//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2023 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class RCCP_InitLoad : EditorWindow {

    [InitializeOnLoadMethod]
    public static void InitOnLoad() {

        EditorApplication.delayCall += EditorDelayedUpdate;
        EditorApplication.playModeStateChanged += EditorPlayModeChanged;

    }

    public static void EditorDelayedUpdate() {

        CheckSymbols();

    }

    public static void EditorPlayModeChanged(PlayModeStateChange state) {

        if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
            return;

        CheckRP();

    }

    public static void CheckSymbols() {

        bool hasKey = false;

#if BCG_RCCP
        hasKey = true;
#endif

        if (!hasKey) {

            EditorUtility.DisplayDialog("Regards from BoneCracker Games", "Thank you for purchasing and using Realistic Car Controller Pro. Please read the documentations before use. Also check out the online documentations for updated info. Have fun :)", "Let's get started!");
            EditorUtility.DisplayDialog("Input System", "RCC Pro is using new input system as default. But you can switch to the old input system later if you want. Make sure your project has Input System installed through the Package Manager now. It should be installed if you have installed dependencies while importing the package. If you haven't installed dependencies, no worries. You can install Input System from the Package Manager (Window --> Package Manager). More info can be found in the documentations.", "Ok");
            RCCP_WelcomeWindow.OpenWindow();

        }

        bool newInputSystemKey = RCCP_Settings.Instance.useNewInputSystem;

        if (newInputSystemKey) {

#if !BCG_NEWINPUTSYSTEM

            RCCP_SetScriptingSymbol.SetEnabled("BCG_NEWINPUTSYSTEM", true);

#endif

        } else {

#if BCG_NEWINPUTSYSTEM

            RCCP_SetScriptingSymbol.SetEnabled("BCG_NEWINPUTSYSTEM", false);

#endif

        }

        RCCP_SetScriptingSymbol.SetEnabled("BCG_RCCP", true);
        RCCP_Installation.Check();

    }

    public static void CheckRP() {

        Shader checkURP = null;

        if (UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline && UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.defaultShader)
            checkURP = UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline.defaultShader;

        if (checkURP != null && checkURP.name == "Universal Render Pipeline/Lit") {

#if !BCG_URP

            int selection = EditorUtility.DisplayDialogComplex("Converting Shaders", "Please read ''RCC Pro - Universal RP (URP)'' to install and configurate URP to your project first. All demo materials need to be converted to URP shader.", "Open DemoMaterials", "Cancel", "");

            if (selection == 0) {

                EditorApplication.ExitPlaymode();

                FileUtil.DeleteFileOrDirectory(RCCP_DemoContent.Instance.GetAssetPath(RCCP_DemoContent.Instance.builtinShadersContent));
                AssetDatabase.Refresh();

                Selection.activeObject = RCCP_DemoMaterials.Instance;
                AssetDatabase.ImportPackage(RCCP_DemoContent.Instance.GetAssetPath(RCCP_DemoContent.Instance.URPShaderPackage), true);
                AssetDatabase.Refresh();

            }

#endif

            RCCP_SetScriptingSymbol.SetEnabled("BCG_URP", true);

        } else {

#if BCG_URP

            int selection = EditorUtility.DisplayDialogComplex("Converting Shaders", "Please read ''RCC Pro - Universal RP (URP)'' to remove URP from your project first. All demo materials need to be converted back to builtin shader (Standard).", "Open DemoMaterials", "Cancel", "");

            if (selection == 0) {

                EditorApplication.ExitPlaymode();

                FileUtil.DeleteFileOrDirectory(RCCP_DemoContent.Instance.GetAssetPath(RCCP_DemoContent.Instance.URPShadersContent));
                AssetDatabase.Refresh();

                Selection.activeObject = RCCP_DemoMaterials.Instance;
                AssetDatabase.ImportPackage(RCCP_DemoContent.Instance.GetAssetPath(RCCP_DemoContent.Instance.builtinShaderPackage), true);
                AssetDatabase.Refresh();

            }

#endif

            RCCP_SetScriptingSymbol.SetEnabled("BCG_URP", false);

        }

    }

}
