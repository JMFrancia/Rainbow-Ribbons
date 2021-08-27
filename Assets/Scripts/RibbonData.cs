using System;
using UnityEngine;

[Serializable]
public struct RibbonData {
    public ColorNames name;
    public Color color;
    public Texture2D ribbonTex;

    public RibbonData(ColorNames name, Color color, Texture2D ribbonTex) {
        this.name = name;
        this.color = color;
        this.ribbonTex = ribbonTex;
    }
}