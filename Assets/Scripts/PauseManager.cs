using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Settings")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    
    private bool isPaused = false;
    
    void Start()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }
    
    private void TogglePause()
    {
        isPaused = !isPaused;
        
        Time.timeScale = isPaused ? 0f : 1f;
        
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(isPaused);
        }
    }
    
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}