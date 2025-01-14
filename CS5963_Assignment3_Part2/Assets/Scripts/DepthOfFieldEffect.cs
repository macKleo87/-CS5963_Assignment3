﻿using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class DepthOfFieldEffect : MonoBehaviour
{
    [HideInInspector]
    public Shader dofShader;

    [Range(0.1f, 100f)]
    public float focusDistance = 10f;

    [Range(0.1f, 10f)]
    public float focusRange = 3f;

    [Range(1f, 10f)]
    public float bokehRadius = 4f;

    /*
    [Range(0, 10)]
    public float intensity = 1;

    [Range(1, 16)]
    public int iterations = 5;
    
    [Range(0, 10)]
    public float threshold = 1;

    [Range(0, 1)]
    public float softThreshold = 0.5f;

    public bool debug;
    */
    const int circleOfConfusionPass = 0;
    const int preFilterPass = 1;
    const int bokehPass = 2;
    const int postFilterPass = 3;
    const int combinePass = 4;

    [NonSerialized]
    Material dofMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        /*
        const int BoxDownPrefilterPass = 0;
        const int BoxDownPass = 1;
        const int BoxUpPass = 2;
        const int ApplyBloomPass = 3;
        const int DebugBloomPass = 4;
        */


        if (dofMaterial == null)
        {
            dofMaterial = new Material(dofShader);
            dofMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        dofMaterial.SetFloat("_BokehRadius", bokehRadius);
        dofMaterial.SetFloat("_FocusDistance", focusDistance);
        dofMaterial.SetFloat("_FocusRange", focusRange);

        RenderTexture coc = RenderTexture.GetTemporary(
        source.width, source.height, 0,
        RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear
        );

        int width = source.width / 2;
        int height = source.height / 2;
        RenderTextureFormat format = source.format;
        RenderTexture dof0 = RenderTexture.GetTemporary(width, height, 0, format);
        RenderTexture dof1 = RenderTexture.GetTemporary(width, height, 0, format);

        dofMaterial.SetTexture("_CoCTex", coc);
        dofMaterial.SetTexture("_DoFTex", dof0);

        Graphics.Blit(source, coc, dofMaterial, circleOfConfusionPass);
        Graphics.Blit(source, dof0);
        Graphics.Blit(dof0, dof1, dofMaterial, bokehPass);
        Graphics.Blit(dof1, dof0, dofMaterial, postFilterPass);
        Graphics.Blit(source, destination, dofMaterial, combinePass);
        //Graphics.Blit(dof0, destination);

        //Graphics.Blit(source, destination, dofMaterial, bokehPass);
        //Graphics.Blit(coc, destination);

        RenderTexture.ReleaseTemporary(coc);
        RenderTexture.ReleaseTemporary(dof0);
        RenderTexture.ReleaseTemporary(dof1);

        /*
        if (bloom == null)
        {
            bloom = new Material(bloomShader);
            bloom.hideFlags = HideFlags.HideAndDontSave;
        }

        //bloom.SetFloat("_Threshold", threshold);
        //bloom.SetFloat("_SoftThreshold", softThreshold);
        float knee = threshold * softThreshold;
        Vector4 filter;
        filter.x = threshold;
        filter.y = filter.x - knee;
        filter.z = 2f * knee;
        filter.w = 0.25f / (knee + 0.00001f);
        bloom.SetVector("_Filter", filter);
        bloom.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));

        RenderTexture[] textures = new RenderTexture[16];
        int width = source.width / 2;
        int height = source.height / 2;
        RenderTextureFormat format = source.format;

        RenderTexture currentDestination = textures[0] = RenderTexture.GetTemporary(width, height, 0, format);

        Graphics.Blit(source, currentDestination, bloom, BoxDownPrefilterPass);
        RenderTexture currentSource = currentDestination;

        int i = 1;
        for (; i < iterations; i++)
        {
            width /= 2;
            height /= 2;
            if (height < 2)
            {
                break;
            }
            currentDestination = textures[i] = RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(currentSource, currentDestination, bloom, BoxDownPass);
            //RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }

        for (i -= 2; i >= 0; i--)
        {
            currentDestination = textures[i];
            textures[i] = null;
            Graphics.Blit(currentSource, currentDestination, bloom, BoxUpPass);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }

        //Graphics.Blit(currentSource, destination, bloom, BoxUpPass);
        if (debug)
        {
            Graphics.Blit(currentSource, destination, bloom, DebugBloomPass);
        }
        else
        {
            bloom.SetTexture("_SourceTex", source);
            Graphics.Blit(currentSource, destination, bloom, ApplyBloomPass);
        }
        RenderTexture.ReleaseTemporary(currentSource);
        */
    }
}