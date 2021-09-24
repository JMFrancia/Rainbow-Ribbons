using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DentedPixel;

public class TextManager : MonoBehaviour
{
    [SerializeField] float _wiggleDist = 2f;
    [SerializeField] float _wiggleSpeed = 2f;
    [SerializeField] float _scale = 1.5f;
    [SerializeField] float _scaleTime = .5f;
    [SerializeField] RibbonManager _ribbonManager;
    [SerializeField] TextMeshProUGUI _redLetter;
    [SerializeField] TextMeshProUGUI _orangeLetter;
    [SerializeField] TextMeshProUGUI _yellowLetter;
    [SerializeField] TextMeshProUGUI _greenLetter;
    [SerializeField] TextMeshProUGUI _blueLetter;
    [SerializeField] TextMeshProUGUI _indigoLetter;
    [SerializeField] TextMeshProUGUI _violetLetter;

    float _originalY;
    ColorNames _activeColor;
    bool _wiggling = false;

    Dictionary<ColorNames, TextMeshProUGUI> _letters;

    public void ActivateLetter(ColorNames color, bool resetActive = true)
    {
        if (resetActive)
        {
            ResetActiveLetter();
        }
        _activeColor = color;
        //LeanTween.value(_letters[_activeColor].gameObject, a => myText.color = a, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1);
        //LeanTween.value(_letters[_activeColor].gameObject, _letters[_activeColor].color, _ribbonManager.RibbonData[_activeColor].color, _scaleTime);
        LeanTween.scale(_letters[_activeColor].gameObject, _letters[_activeColor].transform.localScale * _scale, _scaleTime).setOnComplete(() => _wiggling = true);
    }

    public void ResetActiveLetter()
    {
        Debug.Log("Resetting color " + _activeColor);
        _letters[_activeColor].color = _ribbonManager.RibbonData[_activeColor].color;
        _wiggling = false;
        LeanTween.scale(_letters[_activeColor].gameObject, Vector3.one, _scaleTime);
    }

    private void Awake()
    {
        _letters = new Dictionary<ColorNames, TextMeshProUGUI>(){
            { ColorNames.Red, _redLetter },
            { ColorNames.Orange, _orangeLetter },
            { ColorNames.Yellow, _yellowLetter },
            { ColorNames.Green, _greenLetter },
            { ColorNames.Blue, _blueLetter },
            { ColorNames.Indigo, _indigoLetter },
            { ColorNames.Violet, _violetLetter }
        };
        _originalY = _letters[ColorNames.Red].transform.position.y;
    }

    private void Update()
    {
        if (_wiggling)
        {
            UpdateWiggleAnimation(_letters[_activeColor].transform);
        }
    }

    void UpdateWiggleAnimation(Transform t) {
        t.position += new Vector3(0f, Mathf.Cos(Time.time * _wiggleSpeed) * _wiggleDist, 0f);
    }
}
