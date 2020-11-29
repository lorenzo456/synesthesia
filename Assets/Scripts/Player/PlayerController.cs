using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    //Public Camera Settings
    public float cameraSpeedH;
    public float cameraSpeedV;
    //Private Camera Settings
    private GameObject playerCamera;
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    [Header("Character Settings")]
    //Public Character Settings
    public float playerSpeed;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask layer;

    //Private Character Settings
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded = false;
    private float gravity = -60.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>().gameObject;
        controller = GetComponent<CharacterController>();
        cameraSpeedH = 2.0f;
        cameraSpeedV = 2.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        PlayerMovement();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
    private void PlayerMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float playerH = Input.GetAxis("Horizontal");
        float playerV = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * playerV + transform.right * playerH;

        controller.Move(move * playerSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CameraMovement()
    {
        //Set Pitch and Yaw of the player.
        yaw += Input.GetAxis("Mouse X") * cameraSpeedH;
        pitch += Input.GetAxis("Mouse Y") * cameraSpeedV * -1;

        //Clamp the Pitch
        pitch = Mathf.Clamp(pitch, -70, 70);

        //Set Player to rotate along the yaw
        transform.eulerAngles = new Vector3(0, yaw, 0);

        //Set Camera to rotate along the Pitch
        playerCamera.transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0);
    }
}
