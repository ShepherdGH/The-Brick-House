using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    [HideInInspector] public float speed = 2f;
    [HideInInspector] public Vector3 direction = Vector3.right;
    [HideInInspector] public float despawnDist = 16f;
    
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
        RotateToDir();
    }
    
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (despawnDist < Vector3.Distance(transform.position, startPos))
        {
            Destroy(gameObject);
        }
    }
    
    private void RotateToDir()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            transform.rotation = targetRotation;
        }
    }
    
    public void SetDir(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        RotateToDir();
    }
}