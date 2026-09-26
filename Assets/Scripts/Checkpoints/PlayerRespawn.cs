using UnityEngine;

// Kill zone (spikes, pits, lava): sends the player back to the last checkpoint
public class PlayerRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            CheckpointManager.Respawn(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            CheckpointManager.Respawn(collision.gameObject);
    }
}
