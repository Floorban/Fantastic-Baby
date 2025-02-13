using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] Transform throwTarget;
    [SerializeField] Transform pickUpHolder;
    public SpawnedObj curObj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && curObj != null)
        {
            curObj.duration = 0.4f;
            curObj.maxHeightY = 2f;
            curObj.Throw(throwTarget.position);
            curObj.transform.SetParent(throwTarget);
            curObj = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null && curObj == null)
        {
            collectable.Collect(pickUpHolder);
            curObj = other.GetComponent<SpawnedObj>();
        }
    }
}
