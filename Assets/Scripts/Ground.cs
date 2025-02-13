using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool canSpin;
    [SerializeField] float spinSpeed;
    private void FixedUpdate()
    {
        if (canSpin)
        {
            transform.RotateAround(transform.position, Vector3.up, spinSpeed * Time.fixedDeltaTime);
        }
    }
}
