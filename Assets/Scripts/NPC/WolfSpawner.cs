using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfSpawner : MonoBehaviour
{
    [Header("Spawning Params")]
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private float minSpawn = 2f;
    [SerializeField] private float maxSpawn = 8f;
    [SerializeField] private int maxActiveWolves = 10;
    
    [Header("Particle Effects")]
    [SerializeField] private GameObject spawnEffectPrefab; 
    [SerializeField] private float effectTime = 2f; 
    [SerializeField] private Vector3 effectOffset = new Vector3(0, 0.5f, 0);
    
    [Header("Spawn Locations")]
    [SerializeField] private Transform[] spawnPoints;
    
    [Header("Movement Settings")]
    [SerializeField] private float wolfSpeed = 2f;
    [SerializeField] private float despawnDist = 16f;
    
    private List<GameObject> activeWolves = new List<GameObject>();
    private Coroutine spawnCoroutine;
    
    void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = new Transform[1];
            spawnPoints[0] = transform;
        }
        spawnCoroutine = StartCoroutine(SpawnWolvesRoutine());
    }
    
    void Update()
    {
        activeWolves.RemoveAll(wolf => wolf == null);
    }
    
    private IEnumerator SpawnWolvesRoutine()
    {
        while (true)
        {
            if (activeWolves.Count < maxActiveWolves)
            {
                SpawnWolf();
            }
            float interval = Random.Range(minSpawn, maxSpawn);
            yield return new WaitForSeconds(interval);
        }
    }
    
    private void SpawnWolf()
    {
        int spawnIdx = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIdx];
        
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRotation = spawnPoint.rotation;
        
        if (spawnEffectPrefab != null)
        {
            Vector3 effectPosition = spawnPos + effectOffset;
            GameObject effectInstance = Instantiate(spawnEffectPrefab, effectPosition, Quaternion.identity);
            Destroy(effectInstance, effectTime);
        }
        
        GameObject newWolf = Instantiate(wolfPrefab, spawnPos, spawnRotation);
        WolfMovement movement = newWolf.GetComponent<WolfMovement>();
        
        if (movement == null)
        {
            movement = newWolf.AddComponent<WolfMovement>();
        }
        
        Vector3 moveDir = spawnRotation * Vector3.forward;
        moveDir.y = 0;
        moveDir = moveDir.normalized;
        
        movement.speed = wolfSpeed;
        movement.direction = new Vector3(moveDir.x, 0, moveDir.z).normalized;
        movement.despawnDist = despawnDist;    
        
        activeWolves.Add(newWolf);
    }
}