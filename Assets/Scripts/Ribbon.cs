﻿using UnityEngine;

[ExecuteAlways]
public class Ribbon : MonoBehaviour
{
    [SerializeField] bool _ghost = false;
    [SerializeField] Color _color = Color.red;
    [SerializeField] Color _ghostColor = Color.cyan;
    [SerializeField] float _ghostAlpha = .4f;

    Renderer _renderer;

    public void SetColor(Color color) {
        _color = color;
        //Animate
        _ghost = false;
        UpdateColor();
    }

    void UpdateColor() {
        Material temp = new Material(GetRenderer().sharedMaterial);
        if (_ghost)
        {
            Color color = _ghostColor;
            color.a = _ghostAlpha;
            temp.color = color;
        }
        else
        {
            temp.color = _color;
            temp.SetColor("_EmissionColor", _color / 2);
        }
        GetRenderer().material = temp;
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateColor();
        }
    }

    Renderer GetRenderer() {
        if (_renderer == null) {
            _renderer = GetComponent<Renderer>();
        }
        return _renderer;
    }
}
