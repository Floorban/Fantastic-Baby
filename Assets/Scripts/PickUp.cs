using System;
using UnityEngine;
public interface ICollectable
{
    public void Collect();
}
public class PickUp : MonoBehaviour, ICollectable
{
    public static event Action OnPickup;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Collect()
    {
        Debug.Log("on pickup");
        Destroy(gameObject);
        OnPickup?.Invoke();
    }

}
