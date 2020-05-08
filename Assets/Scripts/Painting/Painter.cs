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

    [HideInInspector]
    public float RadiusMult = 1f;

    public float2 WorldPos2D => new float2(transform.position.x, transform.position.y);

    public void Update()
    {
        // TODO: Remove
        if (Input.GetMouseButton(0))
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
