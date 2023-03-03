/*  Filename:           TimeScaleSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Inherited from BaseSlider that updates time scale according to the slider value
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *                      March 3, 2023 (Yuk Yee Wong): Reset time scale
 */

using UnityEngine;

public class TimeScaleSlider : BaseSlider
{
    protected override void OnEnable()
    {
        value = Time.timeScale;
        UpdateValueLabel();
        base.OnEnable();
        onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onValueChanged.RemoveListener(OnSliderValueChanged);
        Time.timeScale = 1f;
    }

    protected override void OnSliderValueChanged(float value)
    {
        Time.timeScale = value;
        UpdateValueLabel();
    }
}
