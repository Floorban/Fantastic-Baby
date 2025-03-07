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
                player.finalScreen.SetActive(true);
                Debug.Log("game over");
            }
        }

        if (other.GetComponent<SpawnedObj>())
        {
            var food = other.GetComponent<SpawnedObj>();
            if (food.eatable)
            {
                food.gameObject.SetActive(false);
            }
        }
    }
}
