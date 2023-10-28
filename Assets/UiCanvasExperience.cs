using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasExperience : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    public void SetValue(float newValue, float maxValue)
    {
        var nValue = newValue / maxValue;
        print($"{newValue} / {maxValue} = {nValue}");

        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, _slider.value, nValue, 0.25f)
            .setOnUpdate((float val) => {
                _slider.value = val;
            })
            .setEaseOutSine();
    }

}
