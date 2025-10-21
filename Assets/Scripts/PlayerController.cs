using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField]private int playerIndex = 0;  // player 1 or 2
    private string horizontalAxis;
    private string verticalAxis;
    private string takeButton;
    private string useButton;

    [Header("Movement")]
    private CharacterController characterController;
    public float speed = 10f;
    public float rotateSpeed = 1.0f;
    
    private float targetAngle;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    private PlayerInteraction _interaction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (playerIndex == 1 || playerIndex == 2)
        {
            horizontalAxis = "Horizontal_P" + playerIndex;
            verticalAxis = "Vertical_P" + playerIndex;
            takeButton = "Take_P" + playerIndex;
            useButton = "Use_P" + playerIndex;
        }
        else
        {
            Debug.Log("Player Index is not set, should be 1 or 2. Current player index: " + playerIndex);
        }

        _interaction = GetComponent<PlayerInteraction>();
    }


    // Update is called once per frame
    void Update()
    {
        // Player Movement
        
        Vector3 move = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
        characterController.Move(move*speed*Time.deltaTime);

        RotatePlayer(move);
        
        // Taking/Droping items
        if (Input.GetButtonDown(takeButton)) 
        {
            //Debug.Log("Take Button is pressed for Player #" + playerIndex);
            
            // Logic for interaction is in PlayerInteraction
            _interaction.Take();
        }

        // Using/Chopping items
        if (Input.GetButtonDown(useButton))
        {
            //Debug.Log("Use Button is pressed for Player #" + playerIndex);
            
            // Logic for interaction is in PlayerInteraction
            _interaction.Use();
        }
    }

    private void RotatePlayer(Vector3 move)
    {
        if (Input.GetAxis(horizontalAxis) == 0 && Input.GetAxis(verticalAxis) == 0)
        {
            return;
        }
        
        // Player Rotation
        //FIXED LINE
        targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + 180f;
        //OLD LINE 
        //targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
            ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public string GetTakeButton()
    {
        return takeButton;
    }
    
    public string GetUseButton()
    {
        return useButton;
    }
}
