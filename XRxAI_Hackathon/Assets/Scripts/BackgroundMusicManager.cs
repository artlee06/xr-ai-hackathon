using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Background Music Settings")]
    public AudioClip backgroundMusic;
    public float volume = 0.5f;
    public bool playOnStart = true;
    public bool loopMusic = true;
    public bool fadeIn = true;
    public float fadeInDuration = 2f;
    
    [Header("Audio Source Settings")]
    public bool use3DAudio = false;
    public float spatialBlend = 0f; // 0 = 2D, 1 = 3D
    
    private AudioSource audioSource;
    private bool isFading = false;
    
    void Start()
    {
        SetupAudioSource();
        
        if (playOnStart && backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }
    
    void SetupAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.clip = backgroundMusic;
        audioSource.loop = loopMusic;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = use3DAudio ? 1f : spatialBlend;
        audioSource.volume = fadeIn ? 0f : volume;
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogWarning("BackgroundMusicManager: No background music clip assigned!");
            return;
        }
        
        audioSource.Play();
        
        if (fadeIn)
        {
            StartCoroutine(FadeInMusic());
        }
        else
        {
            audioSource.volume = volume;
        }
        
        Debug.Log($"Playing background music: {backgroundMusic.name}");
    }
    
    public void StopBackgroundMusic(bool fadeOut = true)
    {
        if (fadeOut && !isFading)
        {
            StartCoroutine(FadeOutMusic());
        }
        else
        {
            audioSource.Stop();
        }
    }
    
    public void PauseBackgroundMusic()
    {
        audioSource.Pause();
    }
    
    public void ResumeBackgroundMusic()
    {
        audioSource.UnPause();
    }
    
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }
    
    private System.Collections.IEnumerator FadeInMusic()
    {
        isFading = true;
        float currentTime = 0;
        
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, currentTime / fadeInDuration);
            yield return null;
        }
        
        audioSource.volume = volume;
        isFading = false;
    }
    
    private System.Collections.IEnumerator FadeOutMusic()
    {
        isFading = true;
        float startVolume = audioSource.volume;
        float currentTime = 0;
        
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeInDuration);
            yield return null;
        }
        
        audioSource.volume = 0f;
        audioSource.Stop();
        isFading = false;
    }
    
    void OnValidate()
    {
        volume = Mathf.Clamp01(volume);
        fadeInDuration = Mathf.Max(0.1f, fadeInDuration);
    }
}
