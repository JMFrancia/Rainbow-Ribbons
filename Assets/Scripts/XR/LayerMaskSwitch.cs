using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskSwitch : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    private Camera cam;
    private Light light;

    void Awake()
    {
        cam = GetComponent<Camera>();
        light = GetComponent<Light>();
    }

    public void ToggleLayers(bool enable)
    {
        if (enable)
        {
            if (cam != null)
                cam.cullingMask |= mask.value;
            if (light != null)
                light.renderingLayerMask |= mask.value;
        }
        else
        {
            if (cam != null)
                cam.cullingMask &= ~mask.value;
            if (light != null)
                light.renderingLayerMask &= ~mask.value;
        }
    }
}
