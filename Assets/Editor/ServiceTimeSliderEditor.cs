using UnityEditor;

[CustomEditor(typeof(ServiceTimeSlider))]
public class ServiceTimeSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
