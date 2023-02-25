/*  Filename:           TimeScaleSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Inherited from BaseSlider that updates time scale according to the slider value
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class TimeScaleSlider : BaseSlider
{
    protected override void OnSliderValueChanged(float value)
    {
        Time.timeScale = value;
        valueLabel.text = value.ToString("0.00");
    }
}
