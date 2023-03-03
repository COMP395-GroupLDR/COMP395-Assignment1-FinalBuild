/*  Filename:           BaseSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Slider that updates min label, max label, and value label to reflect the value settings
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
 *                      March 3, 2023 (Yuk Yee Wong): Added functions to update labels
 */

using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : Slider
{
    [SerializeField] protected Text minLabel;
    [SerializeField] protected Text maxLabel;
    [SerializeField] protected Text valueLabel;

    protected override void Start()
    {
        base.Start();
        UpdateMinLabel(); 
        UpdateMaxLabel();
    }

    public void UpdateMinLabel()
    {
        minLabel.text = minValue.ToString("0.00");
    }

    public void UpdateMaxLabel()
    {
        maxLabel.text = maxValue.ToString("0.00");
    }

    protected abstract void OnSliderValueChanged(float value);

    protected void UpdateValueLabel()
    {
        valueLabel.text = value.ToString("0.00");
    }
}
