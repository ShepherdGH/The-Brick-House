using UnityEngine;

public class PlayerDetectTrigger : MonoBehaviour
{
    public NPCSystem parentWolf;
    [SerializeField] private string playerTag = "Player";
    
    private void Awake()
    {
        Collider pCollider = GetComponent<Collider>();
        if (!pCollider.isTrigger)
        {
            pCollider.isTrigger = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            parentWolf.OnPlayerEnter(other.transform);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            parentWolf.OnPlayerExit(other.transform);
        }
    }
}