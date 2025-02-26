using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] LevelManager throwTarget;
    [SerializeField] Transform pickUpHolder;
    public SpawnedObj curObj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && curObj != null && throwTarget != null)
        {
            curObj.duration = 0.4f;
            curObj.maxHeightY = 2f;
            curObj.Throw(throwTarget.transform.position);
            curObj.transform.SetParent(throwTarget.transform);
            throwTarget.curProgress += curObj.value;
            throwTarget.spawnedObjs.Remove(curObj);
            curObj = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null && curObj == null)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                collectable.Collect(pickUpHolder);
                curObj = other.GetComponent<SpawnedObj>();
            }
        }
    }
}
