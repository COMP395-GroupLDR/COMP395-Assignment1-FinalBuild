/*  Filename:           ServiceTimeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Inherited from BaseSlider that allows to update OnSliderValueChange
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using System;
using UnityEngine;

public class ServiceTimeSlider : BaseSlider
{
    protected override void OnEnable()
    {
        value = Utilities.MeanServiceTimeInMinutes;
        UpdateValueLabel();
        base.OnEnable();
        onValueChanged.AddListener(OnSliderValueChanged);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    protected override void OnSliderValueChanged(float value)
    {
        Utilities.InterArrivalTimeInMinutes = value;
        UpdateValueLabel();
    }
}
