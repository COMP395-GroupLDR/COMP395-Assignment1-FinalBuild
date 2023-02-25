/*  Filename:           BaseSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        February 25, 2023
 *  Description:        Slider that updates min label, max label, and value label to reflect the value settings
 *  Revision History:   February 25, 2023 (Yuk Yee Wong): Initial script.
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
        minLabel.text = minValue.ToString();
        maxLabel.text = maxValue.ToString();
        onValueChanged.AddListener(OnSliderValueChanged);
    }

    protected abstract void OnSliderValueChanged(float value);
}
