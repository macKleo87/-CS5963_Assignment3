using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BloomEffect : MonoBehaviour
{

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture r = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        //Graphics.Blit(source, destination);
        Graphics.Blit(source, r);
        Graphics.Blit(r, destination);
        RenderTexture.ReleaseTemporary(r);
    }
}