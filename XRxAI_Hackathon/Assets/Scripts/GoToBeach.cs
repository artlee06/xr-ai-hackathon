using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBeach : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string targetSceneName = "SetDressingScene"; // Scene to load
    [SerializeField] private bool useLoadingScreen = false; // Optional: use loading screen
    
    /// <summary>
    /// Public method to be called from UI button onClick event
    /// </summary>
    public void OnBeachButtonClicked()
    {
        LoadSetDressingScene();
    }
    
    /// <summary>
    /// Loads the SetDressingScene
    /// </summary>
    public void LoadSetDressingScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Target scene name is not set! Please assign a scene name in the inspector.");
            return;
        }
        
        Debug.Log($"Loading scene: {targetSceneName}");
        
        try
        {
            SceneManager.LoadScene(targetSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene '{targetSceneName}': {e.Message}");
            Debug.LogError("Make sure the scene is added to Build Settings > Scenes in Build.");
        }
    }
    
    /// <summary>
    /// Alternative method to load scene with loading screen (if implemented)
    /// </summary>
    public void LoadSetDressingSceneWithLoading()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Target scene name is not set! Please assign a scene name in the inspector.");
            return;
        }
        
        Debug.Log($"Loading scene with loading screen: {targetSceneName}");
        
        try
        {
            // Load scene asynchronously (better for loading screens)
            StartCoroutine(LoadSceneAsync(targetSceneName));
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene '{targetSceneName}': {e.Message}");
            Debug.LogError("Make sure the scene is added to Build Settings > Scenes in Build.");
        }
    }
    
    /// <summary>
    /// Coroutine to load scene asynchronously
    /// </summary>
    private System.Collections.IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // You can use asyncLoad.progress (0.0 to 1.0) to update a loading bar
            yield return null;
        }
    }
}
