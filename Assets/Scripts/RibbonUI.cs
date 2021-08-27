using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RibbonUI : MonoBehaviour
{
    public RibbonSlot Slot {
        get {
            return _slot;
        }
        set {
            _slot = value;
        }
    }

    public ColorNames RibbonColor {
        get {
            return _color;
        }
        set {
            _color = value;
        }
    }

    [SerializeField] float _hoverSizeMultiplier = 2f;

    static float _maxRaycastDistance = Mathf.Infinity;

    RibbonSlot _slot;
    ColorNames _color;
    int _colliderMask;
    bool _hovering = false;
    Vector3 _originalScale;

    private void Awake()
    {
        GetComponent<Draggable>().OnDrag += OnDrag;
        GetComponent<Draggable>().OnEndDrag += OnEndDrag;
        _originalScale = transform.localScale;
        _colliderMask = LayerMask.GetMask(Constants.Layers.DROPCOLLIDER);
    }

    void OnDrag(PointerEventData data) {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(data.position), out hit, _maxRaycastDistance, _colliderMask))
        {
            if (hit.collider.gameObject.GetComponent<DropCollider>()?.ID == "lion")
            {
                EnterHover();
            }
            else {
                ExitHover();
            }
        }
        else {
            ExitHover();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(transform.position), Camera.main.transform.forward * 1000000f);
        Gizmos.DrawRay(ray);
    }

    void EnterHover() {
        if (!_hovering)
        {
            Debug.Log("Entering hover");
            transform.localScale = _originalScale * _hoverSizeMultiplier;
            _hovering = true;
        }
    }

    void ExitHover()
    {
        if (_hovering)
        {
            transform.localScale = _originalScale;
            Debug.Log("Exiting hover");
            _hovering = false;
        }
    }

    void OnEndDrag(PointerEventData data) {
        if (_hovering) {
            Debug.Log(_color + " ribbon dropped on Lion");
            //if(GameManager.instance.CheckRibbon(_color)) {
            //    //Correct noise
            //    Destroy(gameObject);
            //} else
            //{
            //    //Exit noise
            //    ExitHover();
            //    ReturnToSlot();
            //}
        }
        else
        {
            ExitHover();
            ReturnToSlot();
        }
    }

    public void ReturnToSlot() {
        LeanTween.move(gameObject, _slot.transform, .5f).setEase(LeanTweenType.easeOutBounce);
    }
}
