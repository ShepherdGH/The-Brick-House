using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;

    // The AudioSource component that will play the sound effects.
    private AudioSource audioSource;

    // Public AudioClips (drag and drop your SFX assets here in the Inspector)
    [Header("BGM")]
    public AudioClip bgAlleyWhiteNoise;
    public AudioClip bgAlleyMusic;
    public AudioClip bgTitle;
    public AudioClip bgLevel;

    [Header("Equipment Sounds")]
    public AudioClip playerBoiling;
    public AudioClip playerCutting;
    public AudioClip playerFrying;
    public AudioClip playerServingDish;
    public AudioClip playerSettingDish;
    public AudioClip playerTakingIngredients;
    public AudioClip playerTakingPlate;
    public AudioClip playerThrowingInTrash;
    public AudioClip playerCutOnce;

    [Header("Game Sounds")] 
    public AudioClip orderAppearing;
    public AudioClip orderDisappearing;

    [Header("UI Sounds")]
    public AudioClip receiptFold;

    [Header("Alleyway Sounds")]
    public AudioClip wolfExploding;
    public AudioClip playerSteakPickup;
    
    private Dictionary<string, AudioClip> sfxDictionary;
    private Dictionary<string, AudioClip> bgmDictionary;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        
        sfxDictionary = new Dictionary<string, AudioClip>
        {
            { "boiling", playerBoiling }, // Not Used
            { "cutting", playerCutting }, // Not Used
            { "frying", playerFrying }, // Implemented
            { "servingDish", playerServingDish }, // Implemented
            { "settingDish", playerSettingDish }, // Implemented
            { "takingIngredients", playerTakingIngredients }, 
            { "takingPlate", playerTakingPlate }, // Implemented
            { "throwingInTrash", playerThrowingInTrash }, // Implemented
            { "cutOnce", playerCutOnce }, // Implemented
            
            { "orderAppearing", orderAppearing }, // Implemented
            { "orderDisappearing", orderDisappearing }, // Implemented

            { "receipt", receiptFold },

            { "wolfExploding", wolfExploding},
            { "wolfSteakPickup", playerSteakPickup},
        };

        bgmDictionary = new Dictionary<string, AudioClip>
        {
            { "alleyWhiteNoise", bgAlleyWhiteNoise },
            { "alleyMusic", bgAlleyMusic},
            { "title", bgTitle },
            { "level", bgLevel },
        };
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(string description)
    {
        if (sfxDictionary.TryGetValue(description, out AudioClip clip) && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SoundManager: No SFX found for: " + description);
        }
    }

    public void PlayBGM(string description)
    {
        if (bgmDictionary.TryGetValue(description, out AudioClip clip) && clip != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("SoundManager: No BGM found for: " + description);
        }
    }


}
