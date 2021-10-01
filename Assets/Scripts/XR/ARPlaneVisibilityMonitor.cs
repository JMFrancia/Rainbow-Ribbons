using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlaneVisibilityMonitor : MonoBehaviour
{
    private LayerMaskSwitch layerMaskSwitch;
    // Start is called before the first frame update
    void Awake()
    {
        layerMaskSwitch = GetComponent<LayerMaskSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        layerMaskSwitch.ToggleLayers(PlaceOnPlaneLP.PlacementStatus != PlaceOnPlaneLP.Status.Placed);
    }
}
