using System;
using UnityEngine;
public interface ICollectable
{
    public void Collect(Transform holder);
}
public class PickUp : MonoBehaviour, ICollectable
{
    public float value;
    public static event Action OnPickup;

    public void Collect(Transform holder)
    {
        if (holder != null)
        {
            Debug.Log("on pickup");
            transform.position = holder.position;
            transform.SetParent(holder);
            OnPickup?.Invoke();
        }
    }

}
