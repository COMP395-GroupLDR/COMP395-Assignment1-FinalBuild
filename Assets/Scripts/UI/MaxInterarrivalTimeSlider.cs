/*  Filename:           MaxInterarrivalTimeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

public class MaxInterarrivalTimeSlider : BaseSlider
{
    private MinInterarrivalTimeSlider minSlider;

    protected override void Start()
    {
        base.Start();
    }

    private void UpdateMinSlider()
    {
        if (minSlider == null)
        {
            minSlider = FindObjectOfType<MinInterarrivalTimeSlider>();
        }
    }

    protected override void OnEnable()
    {
        value = Utilities.MaxInterarrivalTimeInMinutes;
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
        Utilities.MaxInterarrivalTimeInMinutes = value;
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
