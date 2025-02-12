using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private float maxHeightY;

    public Transform target;
    private void Start()
    {
        StartCoroutine(Curve(transform.position, target.position));
    }
    public IEnumerator Curve(Vector3 start, Vector3 finish)
    {
        var timePast = 0f;

        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            var linearTime = timePast / duration;
            var heightTime = curve.Evaluate(linearTime);

            var height = Mathf.Lerp(0f, maxHeightY, heightTime); //clamped between the max height and 0

            transform.position = Vector3.Lerp(start, finish, linearTime) + new Vector3(0f, height, 0f);

            yield return null;
        }
    }
}
