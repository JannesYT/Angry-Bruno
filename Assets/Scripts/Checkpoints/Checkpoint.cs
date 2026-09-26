using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Vector2 spawnOffset = new Vector2(0f, 0.5f); // spawn slightly above the checkpoint
    [SerializeField] private SpriteRenderer flagRenderer;                 // optional: visual feedback
    [SerializeField] private Color inactiveColor = Color.gray;
    [SerializeField] private Color activeColor = Color.green;

    // Always the checkpoint's own position – can't accidentally point back to the start
    public Vector2 SpawnPosition => (Vector2)transform.position + spawnOffset;

    private void Reset() => GetComponent<Collider2D>().isTrigger = true;

    private void Awake()
    {
        if (flagRenderer == null) flagRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() => SetActive(false);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CheckpointManager.Instance != null)
            CheckpointManager.Instance.SetCheckpoint(this);
    }

    public void SetActive(bool active)
    {
        if (flagRenderer != null)
            flagRenderer.color = active ? activeColor : inactiveColor;
    }
}
