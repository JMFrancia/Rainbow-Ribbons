using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonManager : MonoBehaviour
{
    [SerializeField]
    RibbonData[] _ribbonData = new RibbonData[7];

    Dictionary<ColorNames, RibbonData> RibbonData => _ribbonDict;

    Dictionary<ColorNames, RibbonData> _ribbonDict;

    private void Start()
    {
        _ribbonDict = BuildRibbonDict(_ribbonData);
    }

    Dictionary<ColorNames, RibbonData> BuildRibbonDict(RibbonData[] data) {
        Dictionary<ColorNames, RibbonData> result = new Dictionary<ColorNames, RibbonData>();
        for (int n = 0; n < data.Length; n++)
        {
            result[data[n].name] = data[n];
        }
        return result;
    }
}
