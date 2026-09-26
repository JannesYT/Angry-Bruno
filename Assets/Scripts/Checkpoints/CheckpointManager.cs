using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private Transform startPoint;   // optional: where the player spawns before any checkpoint
    private Checkpoint currentCheckpoint;
    private Vector2 fallbackStart;

    // Last checkpoint touched, or the start point if none yet
    public Vector2 RespawnPosition
    {
        get
        {
            if (currentCheckpoint != null) return currentCheckpoint.SpawnPosition;
            if (startPoint != null) return startPoint.position;
            return fallbackStart;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // No start point assigned? Use wherever the player stands when the level begins.
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) fallbackStart = player.transform.position;
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint == null || currentCheckpoint == checkpoint) return;

        if (currentCheckpoint != null) currentCheckpoint.SetActive(false);
        currentCheckpoint = checkpoint;
        currentCheckpoint.SetActive(true);

        Debug.Log($"Checkpoint set: {checkpoint.name} at {checkpoint.SpawnPosition}");
    }

    public void RespawnPlayer(GameObject player)
    {
        Vector2 pos = RespawnPosition;
        Debug.Log($"Respawn at {pos} | Checkpoint: {(currentCheckpoint != null ? currentCheckpoint.name : "none")}");

        if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.position = pos;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        player.transform.position = pos;
    }

    // Use this from any script that kills the player – works even without a manager in the scene.
    public static void Respawn(GameObject player)
    {
        if (Instance == null)
        {
            Debug.LogError("No CheckpointManager in the scene! Add an empty GameObject with the CheckpointManager script.");
            return;
        }
        Instance.RespawnPlayer(player);
    }
}
