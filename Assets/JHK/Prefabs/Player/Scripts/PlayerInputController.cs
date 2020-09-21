using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float OtherValue = 1.0f;

    [SerializeField]
    private GameObject PlayerCamera = null;

    [SerializeField]
    private LayerMask GroundMask = -1;

    [SerializeField]
    private float PlayerMoveSpeed = 1.0f;

    [SerializeField]
    private float PlayerRotateSpeed = 60.0f;

    [SerializeField]
    private float JumpForce = 5.0f;

    private Rigidbody _playerRigidbody = null;

    private float _playerVerticalRotate = 0.0f;
    private float _playerHorizontalRotate = 0.0f;

    #region MonoBehaviour Callbacks
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(PlayerCamera == null) 
        { 
            Debug.Log("Player Camera has Not Attached, Please Check Player Object");
            gameObject.SetActive(false);
        }

        Vector3 playerEulerAngle = PlayerCamera.transform.rotation.eulerAngles;
        _playerVerticalRotate = playerEulerAngle.x;
        _playerHorizontalRotate = playerEulerAngle.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            JumpOnPosition();
        }
    }

    private void FixedUpdate()
    {
        Vector3 nextDirection = GetNextDirectionByKeyInput();
        Vector3 playerVelocity = _playerRigidbody.velocity;

        playerVelocity.x = nextDirection.x * PlayerMoveSpeed;
        playerVelocity.z = nextDirection.z * PlayerMoveSpeed;

        _playerRigidbody.velocity = playerVelocity;
    }

    private void LateUpdate()
    {
        SetPlayerRotationByMouseInput();
    }
    #endregion

    #region Private Method
    private Vector3 GetNextDirectionByKeyInput()
    {
        Vector3 inputVector = Vector3.zero;

        inputVector += Input.GetAxisRaw("Horizontal") * PlayerCamera.transform.right;
        inputVector += Input.GetAxisRaw("Vertical") * PlayerCamera.transform.forward;
        inputVector.y = 0;

        return inputVector.normalized;
    }

    private void SetPlayerRotationByMouseInput()
    {
        _playerVerticalRotate -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * PlayerRotateSpeed;
        _playerHorizontalRotate += Input.GetAxisRaw("Mouse X") * Time.deltaTime * PlayerRotateSpeed;

        Vector3 playerEulerAngle = new Vector3(_playerVerticalRotate, _playerHorizontalRotate, 0);

        PlayerCamera.transform.rotation = Quaternion.Euler(playerEulerAngle);
    }

    private void JumpOnPosition()
    {
        if (CheckOnGround())
            _playerRigidbody.AddForce(transform.up * _playerRigidbody.mass * JumpForce, ForceMode.Impulse);
    }

    private bool CheckOnGround()
    {
        Debug.DrawRay(transform.position, -transform.up * 0.1f, Color.red, 1.0f);
        Collider[] ground = Physics.OverlapSphere(transform.position, 0.1f, GroundMask);

        return ground.Length != 0;
    }
    #endregion
}
