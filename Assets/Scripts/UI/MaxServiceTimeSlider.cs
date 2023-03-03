/*  Filename:           MaxServiceTimeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

public class MaxServiceTimeSlider : BaseSlider
{
    private MinServiceTimeSlider minSlider;

    protected override void Start()
    {
        base.Start();
    }

    private void UpdateMinSlider()
    {
        if (minSlider == null)
        {
            minSlider = FindObjectOfType<MinServiceTimeSlider>();
        }
    }

    protected override void OnEnable()
    {
        value = Utilities.MaxServiceTimeInMinutes;
        UpdateValueLabel();
        UpdateDisplay();
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
        Utilities.MaxServiceTimeInMinutes = value;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        UpdateValueLabel();
        UpdateMinSlider();

        if (minSlider != null)
        {
            minSlider.maxValue = value;
            minSlider.UpdateMaxLabel();
        }
    }
}
