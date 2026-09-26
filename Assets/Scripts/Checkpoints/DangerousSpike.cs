using UnityEngine;

public class DangerousSpike : MonoBehaviour
{
    private SpriteRenderer spikeRenderer;

    private void Awake()
    {
        spikeRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tötet den Spieler NUR, wenn der Sprite Renderer bereits AKTIV (sichtbar) ist
            if (spikeRenderer != null && spikeRenderer.enabled)
            {
                // Respawn am letzten Checkpoint (oder am Start, falls noch keiner berührt wurde)
                CheckpointManager.Respawn(other.gameObject);
            }
        }
    }
}
