using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSetup : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Design aspect ratio
    public float defaultFOV = 60f;        // Default field of view
    public bool adjustFOV = true;         // Toggle to use FOV or camera position

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;

        if (adjustFOV)
        {
            // Adjust field of view for vertical space
            cam.fieldOfView = defaultFOV / scale;
        }
        else
        {
            // Adjust camera distance (move back to maintain view)
            Vector3 pos = cam.transform.position;
            pos.z = -10f * scale; // Change based on your scene setup
            cam.transform.position = pos;
        }
    }
}
