/*  Filename:           CountDownTimer.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private Text label;
    private float timeRemained;

    void Update()
    {
        if (timeRemained > 0)
        {
            timeRemained -= Time.deltaTime;
            if (timeRemained < 0)
            {
                timeRemained = 0;
            }
            UpdateTimelabel();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateTimelabel()
    {
        label.text = Utilities.GetFormattedTime(timeRemained);
    }

    public void CountDown(float time)
    {
        timeRemained = time;
        UpdateTimelabel();
        gameObject.SetActive(true);
    }

}
