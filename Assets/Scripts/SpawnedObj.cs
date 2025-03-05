using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnedObj : PickUp
{
    Camera cam;
    [Header("Properties")]
    [SerializeField] Image sr;

    [Header("Throw Anim")]
    public AnimationCurve curve;
    public float duration;
    public float maxHeightY;
    private void Awake()
    {
        cam = FindFirstObjectByType<Camera>();
    }
    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(cam.transform.position);
        }
    }
    public void Init(Sprite image, Vector3 targetPos)
    {
        if (targetPos == null || image == null) return;
        sr.sprite = image;
        StartCoroutine(Curve(transform.position, targetPos, maxHeightY, duration));
    }
    public void Throw(Vector3 targetPos)
    {
        value = Random.Range(value - 10f,value + 10f);
        StartCoroutine(Curve(transform.position, targetPos, maxHeightY * 5f, duration * 1.5f));
    }
    private IEnumerator Curve(Vector3 start, Vector3 finish, float h, float d)
    {
        var timePast = 0f;
        var dur = Random.Range(d - 0.2f, d + 0.2f);
        while (timePast < dur)
        {
            timePast += Time.deltaTime;

            var linearTime = timePast / dur;
            var heightTime = curve.Evaluate(linearTime);

            var height = Mathf.Lerp(0f, h, heightTime); //clamped between the max height and 0

            transform.position = Vector3.Lerp(start, finish, linearTime) + new Vector3(0f, height, 0f);

            yield return null;
        }
    }
}
