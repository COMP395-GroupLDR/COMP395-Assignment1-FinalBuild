/*  Filename:           MaxInterarrivalTimeSliderEditor.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

using UnityEditor;

[CustomEditor(typeof(MaxInterarrivalTimeSlider))]
public class MaxInterarrivalTimeSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
