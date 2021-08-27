using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RibbonManager : MonoBehaviour
{
    [SerializeField]
    GameObject _ribbonUIPrefab;
    [SerializeField]
    Canvas _canvas;
    [SerializeField]
    RibbonData[] _ribbonData = new RibbonData[7];

    public Dictionary<ColorNames, RibbonData> RibbonData => _ribbonDict;

    Dictionary<ColorNames, RibbonData> _ribbonDict;

    const string RIBBON_SPRITE_PATH = "Sprites/Ribbons/";
    const string RIBBON_FILE_SUFFIX = "_Ribbon_sprite";

    private void Awake()
    {
        _ribbonDict = BuildRibbonDict(_ribbonData);
    }

    public RibbonUI GenerateRibbonUI(ColorNames color) {
        RibbonUI result = Instantiate(_ribbonUIPrefab, _canvas.transform).GetComponent<RibbonUI>();
        result.RibbonColor = color;
        result.GetComponent<Image>().sprite = _ribbonDict[color].ribbonTex;
        return result;
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

