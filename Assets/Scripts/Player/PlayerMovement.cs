using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Speed")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject aimObject;

    private Rigidbody playerRb;
    private Camera mainCamera;
    private Animator playerAnim;

    private Vector3 input;
    private Vector3 inputVelocity;
    
    [Header("Depend on controller or mouse")]
    [Tooltip("Whether to use DualShock 4 Right stick or mouse to look")]
    [SerializeField] private bool usingController = true; // TODO Update this so bool switches based on what player is inputting. Should be in different class?
    // TODO Controller only works with DS4, add support for XB1, X360

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        if (!usingController)
        {
            MakeCameraRay();
        }
        else
        {
            LookUsingController();
        }
    }

    private void UpdateInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputVelocity = input * speed;
        if (input != Vector3.zero)
        {
            playerAnim.SetBool("IsMoving", true);
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
        }
        
    }

    private void MakeCameraRay()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            LookAtCameraRay(cameraRay, rayLength);
        }
    }

    private void LookAtCameraRay(Ray cameraRay, float rayLength)
    {
        Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
    }

    private void LookUsingController()
    {
        Vector3 playerDir = Vector3.right * Input.GetAxisRaw("RStickHorizontal") + Vector3.forward * -Input.GetAxisRaw("RStickVertical"); // RStickVertical needs to be negative
        if (playerDir.sqrMagnitude > 0f) // If controller is receiving input
        {
            transform.rotation = Quaternion.LookRotation(playerDir, Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        //playerRb.velocity = inputVelocity;
        playerRb.AddForce(inputVelocity, ForceMode.Impulse);
    }
}
