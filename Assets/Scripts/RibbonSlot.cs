using UnityEngine;

public class RibbonSlot : MonoBehaviour
{
    [SerializeField] GameObject ribbonPrefab;

    RectTransform _rectTransform;
    RibbonUI _ribbonUI;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void AssignRibbon(RibbonUI ribbon, bool animation) {
        _ribbonUI = ribbon;
        ribbon.GetComponent<RectTransform>().sizeDelta = new Vector2(_rectTransform.rect.width, _rectTransform.rect.width);
        if (animation)
        {
            LeanTween.move(ribbon.gameObject, transform, .2f).setEase(LeanTweenType.notUsed);
        }
        else
        {
            ribbon.transform.position = transform.position;
        }
        ribbon.slot = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        float width = GetComponent<RectTransform>().rect.width;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, width, width));
    }
}
