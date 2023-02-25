/*  Filename:           InterarrivalTimeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Inherited from BaseSlider that allows to update OnSliderValueChange
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 */

using System;

public class InterarrivalTimeSlider : BaseSlider
{
    public Action<float> OnValueChangeCallback;

    protected override void OnSliderValueChanged(float value)
    {
        OnValueChangeCallback?.Invoke(value);
        valueLabel.text = value.ToString("0.00");
    }
}
