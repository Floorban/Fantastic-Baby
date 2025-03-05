using System.Collections;
using FMODUnity;
using UnityEngine;
public class Collector : MonoBehaviour
{
    /*    [SerializeField] float interactRadius;
        [SerializeField] LayerMask interactLayer;*/
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    [SerializeField] LevelManager throwTarget;
    [SerializeField] Transform pickUpHolder;
    public GameObject canPickupObj;
    public SpawnedObj curObj;
    public bool canPickup;

    [SerializeField] FMODUnity.EventReference s_Eating;

    private void Update()
    {
        if (player.transform.localScale.x > 0.3f)
        {
            player.transform.localScale -= Vector3.one * 0.01f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            /*if (curObj == null)
            {
                if (Physics.SphereCast(transform.position, interactRadius, -transform.up, out RaycastHit hitInfo, 1f, interactLayer))
                {
                    Debug.Log(hitInfo.collider.gameObject);
                    if (hitInfo.collider.gameObject.TryGetComponent(out ICollectable interactObj))
                    {
                        interactObj.Collect(pickUpHolder);
                        curObj = hitInfo.collider.gameObject.GetComponent<SpawnedObj>();
                    }
                }
            }*/
            if (canPickup)
            {
                //canPickupObj.GetComponent<ICollectable>().Collect(pickUpHolder);
                canPickupObj.transform.position = pickUpHolder.position;
                canPickupObj.transform.SetParent(pickUpHolder);
                curObj = canPickupObj.GetComponent<SpawnedObj>();
                canPickup = false;
                canPickupObj = null;
            }
            else if (curObj != null && throwTarget != null)
            {
                if (curObj == null) return;
                curObj.duration = 0.4f;
                curObj.maxHeightY = 2f;
                curObj.Throw(throwTarget.transform.position);
                curObj.transform.SetParent(throwTarget.transform);
                throwTarget.curProgress += curObj.value;
                throwTarget.spawnedObjs.Remove(curObj);
                curObj = null;
                canPickupObj = null;
                canPickup = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curObj != null)
            {
                if (curObj.eatable)
                {
                    RuntimeManager.PlayOneShot(s_Eating, transform.position);
                    animator.SetTrigger("Eating");
                    Destroy(curObj.gameObject, 0.8f);
                    curObj = null;
                    canPickupObj = null;
                    StartCoroutine(Enlarge());
                }
                else
                {
                    if (player.transform.localScale.x >= 1)
                    {
                        RuntimeManager.PlayOneShot(s_Eating, transform.position);
                        animator.SetTrigger("Eating");
                        Destroy(curObj.gameObject, 0.8f);
                        curObj = null;
                        canPickupObj = null;
                    }
                }
            }
        }
    }
    IEnumerator Enlarge()
    {
        yield return new WaitForSeconds(0.8f);
        player.transform.localScale *= 1.2f;
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null && curObj == null)
        {
            /*collectable.Collect(pickUpHolder);
            curObj = other.GetComponent<SpawnedObj>();*/
            canPickupObj = other.gameObject;
            canPickup = true;
        }
    }
/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position -transform.up, interactRadius);
    }*/
}
