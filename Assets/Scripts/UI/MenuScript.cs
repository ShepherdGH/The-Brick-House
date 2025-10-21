using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Canvas creditsCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            SceneManager.LoadScene("VictoryScene");
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }


    public void _btn_LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void _btn_Quit()
    {
        Application.Quit();
    }

    public void _btn_LoadCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
    }

    public void _btn_ExitCredits()
    {
        creditsCanvas.gameObject.SetActive(false);
    }
}
