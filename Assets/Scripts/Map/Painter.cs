using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [Serializable]
    public struct PainterData
    {
        public int Radius;
        public Color Color;
    }

    public PainterData PaintingData;
    public PaintableSurfaceTexture Surface;
    public bool ReleasePaint;

    [HideInInspector]
    public float RadiusMult = 1f;

    public float2 WorldPos2D => new float2(transform.position.x, transform.position.y);

    public void Start()
    {

    }

    public void Update()
    {
        if (Surface == null)
        {
            var go = GameObject.FindGameObjectWithTag("MainSurface");
            
            if (go == null) return;

            Surface = go.GetComponent<PaintableSurfaceTexture>();
        }
        
        if (ReleasePaint || Surface != null)
        {
            Paint(Surface);
        }
    }

    public void Paint(PaintableSurfaceTexture surface)
    {
        surface.PaintSurface(new PaintSurfaceInfo
        {
            WorldPos = WorldPos2D,
            Radius = (int)(PaintingData.Radius * RadiusMult),
            Color = PaintingData.Color
        });
    }
}
