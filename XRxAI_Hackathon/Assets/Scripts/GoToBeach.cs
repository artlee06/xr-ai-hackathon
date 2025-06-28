using UnityEngine;

public class GoToBeach : MonoBehaviour
{
    [Header("Beach Environment Settings")]
    [SerializeField] private GameObject beachPrefab; // Assign the beach environment prefab in inspector
    [SerializeField] private float moveSpeed = 5f; // Speed at which the beach moves towards player
    [SerializeField] private float targetDistance = 10f; // Distance from player where beach should stop
    
    private GameObject currentBeachInstance;
    private bool isMovingBeach = false;
    private Transform playerTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player (assuming it's the main camera or a player object)
        playerTransform = Camera.main?.transform;
        if (playerTransform == null)
        {
            // Fallback: try to find a player object
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the beach towards the player if it's currently moving
        if (isMovingBeach && currentBeachInstance != null && playerTransform != null)
        {
            MoveBeachTowardsPlayer();
        }
    }
    
    /// <summary>
    /// Public method to be called from UI button onClick event
    /// </summary>
    public void OnBeachButtonClicked()
    {
        if (beachPrefab == null)
        {
            Debug.LogError("Beach prefab is not assigned! Please assign it in the inspector.");
            return;
        }
        
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found! Make sure there's a main camera or player object.");
            return;
        }
        
        // If there's already a beach instance, destroy it
        if (currentBeachInstance != null)
        {
            Destroy(currentBeachInstance);
        }
        
        // Create the beach environment at a distance from the player
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 20f;
        currentBeachInstance = Instantiate(beachPrefab, spawnPosition, Quaternion.identity);
        
        // Start moving the beach towards the player
        isMovingBeach = true;
        
        Debug.Log("Beach environment is being brought towards the player!");
    }
    
    /// <summary>
    /// Moves the beach instance towards the player
    /// </summary>
    private void MoveBeachTowardsPlayer()
    {
        if (currentBeachInstance == null || playerTransform == null) return;
        
        Vector3 directionToPlayer = (playerTransform.position - currentBeachInstance.transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(currentBeachInstance.transform.position, playerTransform.position);
        
        // If we're close enough to the target distance, stop moving
        if (distanceToPlayer <= targetDistance)
        {
            isMovingBeach = false;
            Debug.Log("Beach environment has reached the target position!");
            return;
        }
        
        // Move the beach towards the player
        currentBeachInstance.transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
    }
    
    /// <summary>
    /// Public method to stop the beach movement
    /// </summary>
    public void StopBeachMovement()
    {
        isMovingBeach = false;
    }
    
    /// <summary>
    /// Public method to remove the beach environment
    /// </summary>
    public void RemoveBeach()
    {
        if (currentBeachInstance != null)
        {
            Destroy(currentBeachInstance);
            currentBeachInstance = null;
        }
        isMovingBeach = false;
    }
}
