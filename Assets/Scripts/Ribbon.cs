using UnityEngine;

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
    }

    private void Update()
    {
        if (_ghost)
        {
            Color color = _ghostColor;
            color.a = _ghostAlpha;
            GetRenderer().material.color = color;
        }
        else {
            GetRenderer().material.color = _color;
            GetRenderer().material.SetColor("_EmissionColor", _color / 2);
        }
        GetRenderer().material.color = _color;
    }

    Renderer GetRenderer() {
        if (_renderer == null) {
            _renderer = GetComponent<Renderer>();
        }
        return _renderer;
    }
}
