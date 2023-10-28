using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAction : MonoBehaviour
{
    public void ChangeVoluem()
    {
        SoundController.Instance.ChangeVolume();
    }
}
