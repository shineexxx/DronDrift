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
/// Light of the vehicle with different types such as headlight, brakelight, turnlight, reverselight, taillight, etc...
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Addons/RCCP Light")]
public class RCCP_Light : RCCP_Component {

    //  Light source.
    private Light _lightSource;
    private Light LightSource {

        get {

            if (_lightSource == null)
                _lightSource = GetComponent<Light>();

            return _lightSource;

        }

    }

    //  Light types and renderer mode.
    public enum LightType { Headlight_LowBeam, Headlight_HighBeam, Brakelight, Taillight, Reverselight, IndicatorLeftLight, IndicatorRightLight }
    public LightType lightType = LightType.Headlight_LowBeam;
    public LightRenderMode lightRendererMode = LightRenderMode.Auto;        //  Pixel light on auto / force pixel mode. Vertex light mode on force vertex.
    public bool overrideRenderMode = false;
    [Space()]
    [Range(.1f, 10f)] public float intensity = 1f;      //  Intensity of the light.
    [Range(.1f, 1f)] public float smoothness = .5f;     //  Smoothness of the intensity.
    [Space()]
    public MeshRenderer emissiveRenderer;       //  Emissive renderer.
    public int emissiveMaterialIndex = 0;       //  Target material index in the emissive renderer.
    public Color emissiveColor = Color.white;       //  Target color for the emissive.
    [Space()]
    private LensFlare lensFlare;        //  Lens flare.
    [Range(0f, 10f)] public float flareBrightness = 1.5f;        //  Max flare brigthness of the light.
    private float finalFlareBrightness;     //  Calculated final flare brightness of the light.
    [Space()]
    public bool isBreakable = true;     //	Can it break at certain damage?
    public float strength = 100f;       //  	Strength of the light. 
    public int breakPoint = 35;     //	    Light will be broken at this point.

    private float orgStrength = 100f;       //	Original strength of the light. We will be using this original value while restoring the light.
    public bool broken = false;        //	Is this light broken currently?

    public override void Start() {

        base.Start();

        //  Getting light and lens flare components.
        lensFlare = GetComponent<LensFlare>();
        orgStrength = strength;

        //  Setting render mode of the light.
        if (!overrideRenderMode) {

            switch (lightType) {

                case LightType.Headlight_LowBeam:

                    if (RCCP_Settings.Instance.useHeadLightsAsVertexLights)
                        lightRendererMode = LightRenderMode.ForceVertex;
                    else
                        lightRendererMode = LightRenderMode.ForcePixel;

                    break;

                case LightType.Brakelight:
                case LightType.Taillight:

                    if (RCCP_Settings.Instance.useBrakeLightsAsVertexLights)
                        lightRendererMode = LightRenderMode.ForceVertex;
                    else
                        lightRendererMode = LightRenderMode.ForcePixel;

                    break;

                case LightType.Reverselight:

                    if (RCCP_Settings.Instance.useReverseLightsAsVertexLights)
                        lightRendererMode = LightRenderMode.ForceVertex;
                    else
                        lightRendererMode = LightRenderMode.ForcePixel;

                    break;

                case LightType.IndicatorLeftLight:
                case LightType.IndicatorRightLight:

                    if (RCCP_Settings.Instance.useIndicatorLightsAsVertexLights)
                        lightRendererMode = LightRenderMode.ForceVertex;
                    else
                        lightRendererMode = LightRenderMode.ForcePixel;

                    break;

            }

        }

    }

    private void Update() {

        //  If no light manager found, return.
        if (!CarController.Lights)
            return;

        //  Light types. Intensity of the light will be adjusted with bools. If corresponding bool is enabled,  intensity of the light will be set to target intensity value.
        switch (lightType) {

            case LightType.Headlight_LowBeam:

                if (CarController.Lights.lowBeamHeadlights)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

            case LightType.Headlight_HighBeam:

                if (CarController.Lights.highBeamHeadlights)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

            case LightType.Brakelight:

                float tailIntensity = 0f;

                if (!CarController.Lights.tailLightFound) {

                    if (CarController.Lights.lowBeamHeadlights)
                        tailIntensity = .3f;
                    else
                        tailIntensity = 0f;

                }

                if (CarController.Lights.brakeLights)
                    Lighting(Mathf.Clamp(intensity + tailIntensity, 0f, intensity));
                else
                    Lighting(tailIntensity);

                break;

            case LightType.Taillight:

                if (CarController.Lights.lowBeamHeadlights)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

            case LightType.Reverselight:

                if (CarController.Lights.reverseLights)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

            case LightType.IndicatorLeftLight:

                if ((CarController.Lights.indicatorsLeft || CarController.Lights.indicatorsAll) && CarController.Lights.indicatorTimer < .5f)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

            case LightType.IndicatorRightLight:

                if ((CarController.Lights.indicatorsRight || CarController.Lights.indicatorsAll) && CarController.Lights.indicatorTimer < .5f)
                    Lighting(intensity);
                else
                    Lighting(0f);

                break;

        }

        //  If emissive renderer selected, enable the necessary keyword for the emissive and set color of it.
        if (emissiveRenderer) {

            emissiveRenderer.materials[emissiveMaterialIndex].EnableKeyword("_EMISSION");        //  Enabling keyword of the material for emission.
            emissiveRenderer.materials[emissiveMaterialIndex].SetColor("_EmissionColor", emissiveColor * LightSource.intensity);

        }

        //  Setting render mode of the light.
        LightSource.renderMode = lightRendererMode;

        if (lensFlare)
            LensFlare();

    }

    /// <summary>
    /// Adjusts intensity of the light smoothly.
    /// </summary>
    /// <param name="_intensity"></param>
    private void Lighting(float _intensity) {

        //  If light is broken, set it to 0. Otherwise, set it to target intensity.
        if (!broken)
            LightSource.intensity = Mathf.Lerp(LightSource.intensity, _intensity, Time.deltaTime * smoothness * 100f);
        else
            LightSource.intensity = Mathf.Lerp(LightSource.intensity, 0f, Time.deltaTime * smoothness * 100f);

    }

    /// <summary>
    /// Operating lensflares related to camera angle.
    /// </summary>
    private void LensFlare() {

        //  If no main camera found, return.
        if (!Camera.main)
            return;

        //  Lensflares are not affected by collider of the vehicle. They will ignore it. Below code will calculate the angle of the light-camera, and sets intensity of the lensflare.
        float distanceTocam = Vector3.Distance(transform.position, Camera.main.transform.position);
        float angle = Vector3.Angle(transform.forward, Camera.main.transform.position - transform.position);

        if (!Mathf.Approximately(angle, 0f))
            finalFlareBrightness = flareBrightness * (4f / distanceTocam) * ((300f - (3f * angle)) / 300f) / 3f;
        else
            finalFlareBrightness = flareBrightness;

        lensFlare.brightness = finalFlareBrightness * LightSource.intensity;
        lensFlare.color = LightSource.color;

    }

    /// <summary>
    /// Listening vehicle collisions for braking the light.
    /// </summary>
    /// <param name="impulse"></param>
    public void OnCollision(float impulse) {

        //  Return if component is disabled.
        if (!enabled)
            return;

        // If light is broken, return.
        if (broken)
            return;

        //	Decreasing strength of the light related to collision impulse.
        strength -= impulse * 20f;
        strength = Mathf.Clamp(strength, 0f, Mathf.Infinity);

        //	Check joint of the part based on strength.
        if (strength <= breakPoint)
            broken = true;

    }

    /// <summary>
    /// Repairs, and restores the light.
    /// </summary>
    public void OnRepair() {

        //  Return if component is disabled.
        if (!enabled)
            return;

        strength = orgStrength;
        broken = false;

    }

    /// <summary>
    /// Gets the color of the light.
    /// </summary>
    /// <param name="color"></param>
    public Color GetLightColor() {

        return LightSource.color;

    }

    /// <summary>
    /// Changes the color of the light.
    /// </summary>
    /// <param name="color"></param>
    public void SetLightColor(Color color) {

        LightSource.color = color;

    }

    public void Reload() {

        LightSource.intensity = 0f;
        finalFlareBrightness = 0f;

    }

}
