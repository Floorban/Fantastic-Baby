using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public IEnumerator Zoom(float zoomSpeed, float startFOV, float targetFieldOfView, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFieldOfView, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = targetFieldOfView;
    }
}
