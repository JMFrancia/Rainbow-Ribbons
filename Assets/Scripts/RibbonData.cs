using System;
using UnityEngine;

[Serializable]
public struct RibbonData {
    public ColorNames name;
    public Color color;
    public Sprite ribbonTex;

    public RibbonData(ColorNames name, Color color, Sprite ribbonTex) {
        this.name = name;
        this.color = color;
        this.ribbonTex = ribbonTex;
    }
}