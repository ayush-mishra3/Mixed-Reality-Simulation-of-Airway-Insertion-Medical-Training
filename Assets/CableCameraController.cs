using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCameraController : MonoBehaviour
{
    public Transform cableTip; // Reference to the tip of the cable
    public float rotationSpeed = 10f;
    public float minVerticalAngle = -90f; // Minimum vertical rotation angle
    public float maxVerticalAngle = 90f; // Maximum vertical rotation angle

    private Camera cableCamera;
    private RenderTexture cameraRenderTexture;

    private float currentHorizontalAngle = 0f; // Current horizontal rotation angle
    private float currentVerticalAngle = 0f; // Current vertical rotation angle

    void Start()
    {
        cableCamera = GetComponent<Camera>();

        // Create and assign a Render Texture to the camera
        cameraRenderTexture = new RenderTexture(1920, 1080, 24);
        cableCamera.targetTexture = cameraRenderTexture;
    }

    void Update()
    {
        // Get input for camera rotation from user's hand or controller
        float horizontalRotation = Input.GetAxis("Horizontal");
        float verticalRotation = Input.GetAxis("Vertical");

        // Rotate the camera based on input
        currentHorizontalAngle += horizontalRotation * rotationSpeed * Time.deltaTime;
        currentVerticalAngle += verticalRotation * rotationSpeed * Time.deltaTime;

        // Clamp vertical angle within the defined range
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);

        // Apply rotation to the camera
        transform.rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);

        // Position the camera at the tip of the cable
        transform.position = cableTip.position;

        // Update the cable camera's view matrix
        cableCamera.worldToCameraMatrix = transform.worldToLocalMatrix;

        // Display the camera's view on a plane or quad object using the Render Texture
        // (Attach the Render Texture to the material of the display object)
    }
}
