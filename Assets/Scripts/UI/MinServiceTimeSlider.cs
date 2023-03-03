/*  Filename:           MinServiceTimeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

public class MinServiceTimeSlider : BaseSlider
{
    protected override void OnEnable()
    {
        value = Utilities.MinServiceTimeInMinutes;
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
        Utilities.MinServiceTimeInMinutes = value;
        UpdateValueLabel();
    }
}
