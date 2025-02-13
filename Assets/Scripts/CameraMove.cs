using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    Camera cam;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] float smoothTime;
    Vector3 velocity = Vector3.zero;
    public Transform target;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
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
