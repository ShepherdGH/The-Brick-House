using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSystem : MonoBehaviour
{
    [SerializeField] private bool playerDetection = false;
    [SerializeField] private string playerTag = "Player";
    
    [SerializeField] private GameObject playerDetectObj;
    
    [Header("Kill Params")]
    [SerializeField] private ParticleSystem deathEffect; 
    
    [Header("Steak Drop Params")]
    [SerializeField] private GameObject steakPrefab;  
    [SerializeField] private float dropOffset = 0.5f; 
    
    private List<Transform> playersInRange = new List<Transform>();
    private List<PlayerController> pControlsInRange = new List<PlayerController>();
    private bool dropSteak = true;
    
    void Start()
    {
        if (playerDetectObj == null)
        {
            Transform detectTransform = transform.Find("PlayerDetect");
            if (detectTransform != null)
            {
                playerDetectObj = detectTransform.gameObject;
                
                PlayerDetectTrigger trigger = playerDetectObj.GetComponent<PlayerDetectTrigger>();
                if (trigger == null)
                {
                    trigger = playerDetectObj.AddComponent<PlayerDetectTrigger>();
                    trigger.parentWolf = this;
                }
            }
        }
    }

    void Update()
    {
        if (playersInRange.Count > 0)
        {
            foreach (PlayerController playerController in pControlsInRange)
            {
                string takeButton = playerController.GetTakeButton();
                if (Input.GetButtonDown(takeButton))
                {
                    KillNPC();
                    break; 
                }
            }
        }
    }
    
    private void KillNPC()
    {
        SoundManager.S.PlaySoundEffect("wolfExploding");
        if (dropSteak)
        {
            DropSteak();
        }
        if (deathEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effectInstance.Play();
            float duration = effectInstance.main.duration;
            Destroy(effectInstance.gameObject, duration + 1.0f);
        }
        Destroy(gameObject);
    }
    
    private void DropSteak()
    {
        if (steakPrefab != null)
        {
            Vector3 dropPosition = transform.position;
            dropPosition.y = dropOffset;
           
            Quaternion dropRotation;
            dropRotation = Quaternion.identity;
            GameObject steak = Instantiate(steakPrefab, dropPosition, dropRotation);
        }
    }
    
    public void OnPlayerEnter(Transform player)
    {
        if (!playersInRange.Contains(player))
        {
            playersInRange.Add(player);
            
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                pControlsInRange.Add(playerController);
            }
            playerDetection = true;
        }
    }
    
    public void OnPlayerExit(Transform player)
    {
        if (playersInRange.Contains(player))
        {
            int index = playersInRange.IndexOf(player);
            playersInRange.Remove(player);
            
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                pControlsInRange.Remove(playerController);
            }
            if (playersInRange.Count == 0)
            {
                playerDetection = false;
            }
        }
    }
}