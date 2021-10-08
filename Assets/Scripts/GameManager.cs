using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RibbonManager _ribbonManager;
    [SerializeField] Transform _ribbonSlotsGridTransform;
    [SerializeField] TextManager _textManager;
    [SerializeField] Ribbon[] _ribbons; //Expected in order of completion
    [SerializeField] ParticleSystem _confettiPS; 

    public static GameManager Instance;

    public ColorNames CurrentColor => _colorNames[_colorIndex];

    RibbonSlot[] _ribbonSlots;
    int _colorIndex;
    ColorNames[] _colorNames = new ColorNames[7];

    public void CorrectChoice() {
        Debug.Log("Correct!");
        _ribbons[_colorIndex].GetComponent<Ribbon>().SetColor(_ribbonManager.RibbonData[CurrentColor].color);
        _colorIndex++;
        if (_colorIndex < _colorNames.Length)
        {
            RequestColor(_colorIndex);
        }
        else
        {
            _textManager.ResetActiveLetter();
            GameOver();
        }
    }

    public void IncorrectChoice() {
        Debug.Log("Wrong choice!");
    }

    public void GameOver() {
        _confettiPS.Play();
        Debug.Log("Game over!");
    }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start()
    {
        _ribbonSlots = _ribbonSlotsGridTransform.GetComponentsInChildren<RibbonSlot>();
        _colorNames = (ColorNames[])System.Enum.GetValues(typeof(ColorNames));
        _colorIndex = 0;
        LoadAllRibbons(_ribbonSlots);
        RequestColor(_colorIndex);
    }

    void LoadAllRibbons(RibbonSlot[] slots) {
        for(int n = 0; n < _colorNames.Length; n++) {
            _ribbonSlots[n].AssignRibbon(_ribbonManager.GenerateRibbonUI(_colorNames[n]), true);            
        }
    }

    void RequestColor(int index) {
        Debug.Log("Requesting color: " + _colorNames[index]);
        _textManager.ActivateLetter(CurrentColor, CurrentColor != ColorNames.Red);
    }
}
