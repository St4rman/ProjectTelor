using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    private CinemachineFollow virCamFollow;
    public float rotateSpeed = 0.25f;
    public float runningSpeed = 30f;
    public float walkingSpeed = 15f;
    [SerializeField] private float fieldOfViewMin = 10;
    [SerializeField] private float fieldOfViewMax = 50;
    [SerializeField] private float followOffsetwMin = 5f;
    [SerializeField] private float followOffsetwMax = 50f;

    private float speed;
    private bool dragPanMoveActive;
    private Vector2 lastMousePos;
    private float targetFieldOfView = 50;
    private Vector3 followOffset;

    void Awake()
    {
        virCamFollow = virtualCamera.GetComponent<CinemachineFollow>();
        followOffset = virCamFollow.FollowOffset;
    }
    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoomFOV();
        HandleCameraZoom();
        //HandleCameraZoomLower();

    }

    private void HandleCameraMovement()
    {
        if (InputManager.Instance.GetSprint())
            speed = runningSpeed;
        else
            speed = walkingSpeed;

        float horizInput = Input.GetAxisRaw("Horizontal");
        float vertInput =  Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.forward * vertInput + transform.right * horizInput;
        transform.position += speed * Time.deltaTime * moveDir;
    }

    private void HandleCameraRotation()
    {
        float rotateDir = InputManager.Instance.GetRotation().x;

        if (Input.GetMouseButtonDown(2))
        {
            dragPanMoveActive = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePos;
            float dragPanSpeed = 0.5f;
            rotateDir = mouseMovementDelta.x * dragPanSpeed;
            lastMousePos = Input.mousePosition;
        }

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed, 0);
    }

    private void HandleCameraZoomFOV()
    {

        if (Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y > 0)
            targetFieldOfView -= 5;

        if (Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y < 0)
            targetFieldOfView += 5;

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        float zoomSpeed = 10f;
        virtualCamera.Lens.FieldOfView =
            Mathf.Lerp(virtualCamera.Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void HandleCameraZoom()
    {
        Vector3 zoomDir = followOffset.normalized;
        float zoomAmount = 3f;
        if (!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y > 0)
            followOffset -= zoomDir * zoomAmount;

        if (!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y < 0)
            followOffset += zoomDir * zoomAmount;

        if (followOffset.magnitude < followOffsetwMin)
            followOffset = zoomDir * followOffsetwMin;

        if (followOffset.magnitude > followOffsetwMax)
            followOffset = zoomDir * followOffsetwMax;

        float zoomSpeed = 20f;
        virCamFollow.FollowOffset =
            Vector3.Lerp(virCamFollow.FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }
    
    private void HandleCameraZoomLower() // lowers the cam, can be used but not rn
    {
        float zoomAmount = 3f;
        if (!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y > 0)
            followOffset.y -= zoomAmount;

        if (!Input.GetKey(KeyCode.LeftControl) && Input.mouseScrollDelta.y < 0)
            followOffset.y += zoomAmount;

        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetwMin, followOffsetwMax);

        float zoomSpeed = 20f;
        virCamFollow.FollowOffset =
            Vector3.Lerp(virCamFollow.FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }
}
