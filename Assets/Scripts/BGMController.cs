using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private string bgm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (bgm)
        {
            case "Level":
                if (SoundManager.S != null) SoundManager.S.PlayBGM("level");
                break;
            case "Title":
                if (SoundManager.S != null) SoundManager.S.PlayBGM("title");
                break;
            case "Alley":
                if (SoundManager.S != null) SoundManager.S.PlayBGM("alleyMusic");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
