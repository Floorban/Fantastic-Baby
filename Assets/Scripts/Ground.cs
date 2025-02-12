using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] bool canSpin;
    [SerializeField] float spinSpeed;
    private void FixedUpdate()
    {
        if (canSpin)
        {
            transform.RotateAround(transform.position, Vector3.forward, spinSpeed * Time.fixedDeltaTime);
        }
    }
}
