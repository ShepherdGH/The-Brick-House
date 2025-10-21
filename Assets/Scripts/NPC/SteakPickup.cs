using UnityEngine;
using System.Collections;

public class SteakPickup : MonoBehaviour
{
    [Header("Pickup Params")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float pickupRadius = 2.0f;
    [SerializeField] private float pickupDelay = 3.5f;

    [Header("Particle Effects")]
    [SerializeField] private GameObject pickupEffectPrefab; 
    [SerializeField] private float effectTime = 2.0f; 
    
    private SphereCollider sphereDetector;
    private bool canPickUp = false;
    private GameObject indicatorInstance;
    
    void Start()
    {
        sphereDetector = GetComponent<SphereCollider>();
        if (sphereDetector == null)
        {
            sphereDetector = gameObject.AddComponent<SphereCollider>();
            sphereDetector.isTrigger = true;
        }
        sphereDetector.radius = pickupRadius;
        sphereDetector.enabled = false;
        StartCoroutine(PickupDelay());
    }
    
    private IEnumerator PickupDelay()
    {
        yield return new WaitForSeconds(pickupDelay);
        canPickUp = true;
        sphereDetector.enabled = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && canPickUp)
        {
            PickupSteak(other.gameObject);
        }
    }
    
    void PickupSteak(GameObject player)
    {
        SoundManager.S.PlaySoundEffect("wolfSteakPickup");
        
        if (pickupEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(
                pickupEffectPrefab,
                transform.position,
                Quaternion.identity
            );
            Destroy(effectInstance, effectTime);
        }
        if (SteakGameManager.Instance != null)
        {
            SteakGameManager.Instance.AddScore(player);
        }
        Destroy(gameObject);
    }
}