using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasExperience : MonoBehaviour
{
    public static UiCanvasExperience Instance;

    [SerializeField]
    private Slider _slider;

    public UiCanvasExperience()
    {
        Instance = this;
    }

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    public void SetValue(float newValue, float maxValue)
    {
        var nValue = newValue / maxValue;
        // short fix for newvalue = 0, which leads to nValue being infinite
        if (nValue > maxValue)
            nValue = 0;
        
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, _slider.value, nValue, 0.25f)
            .setOnUpdate((float val) => {
                _slider.value = val;
            })
            .setEaseOutSine();
    }

}
