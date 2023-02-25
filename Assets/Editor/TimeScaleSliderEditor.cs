using UnityEditor;

[CustomEditor(typeof(TimeScaleSlider))]
public class TimeScaleSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();
    }
}