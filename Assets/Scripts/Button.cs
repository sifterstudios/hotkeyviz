using TMPro;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject _border;
    [SerializeField] TMP_Text _text;
    [SerializeField] bool _changeable;
    [SerializeField] int _row;
    [SerializeField] int _column;

    void OnMouseEnter()
    {
        _border.SetActive(true);
    }

    void OnMouseExit()
    {
        _border.SetActive(false);
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        _text = GetComponentInChildren<TMP_Text>();
        var transformLocalScale = _text.transform.localScale;
        var localScale = transformLocalScale;
        var x = localScale.x;
        var y = localScale.y;
        if (x > y)
            transformLocalScale.x = y;
        else
            transformLocalScale.y = x;
    }
#endif
}