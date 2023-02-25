using UnityEditor;

[CustomEditor(typeof(InterarrivalTimeSlider))]
public class InterarrivalTimeSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
