using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            var player = other.GetComponent<PlayerController>();
            if (player.isGrounded)
            {
                player.gameObject.SetActive(false);
                Debug.Log("game over");
            }
        }
    }
}
