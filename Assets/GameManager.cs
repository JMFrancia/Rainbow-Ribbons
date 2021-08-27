using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RibbonManager _ribbonManager;
    [SerializeField] Transform _ribbonSlotsGridTransform;

    RibbonSlot[] _ribbonSlots;

    private void Start()
    {
        _ribbonSlots = _ribbonSlotsGridTransform.GetComponentsInChildren<RibbonSlot>();
        LoadAllRibbons(_ribbonSlots);
    }

    void LoadAllRibbons(RibbonSlot[] slots) {
        ColorNames[] colors = (ColorNames[]) System.Enum.GetValues(typeof(ColorNames));
        for(int n = 0; n < colors.Length; n++) {
            _ribbonSlots[n].AssignRibbon(_ribbonManager.GenerateRibbonUI(colors[n]), true);            
        }
    }
}
