using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerCamera = null;

    [SerializeField]
    private float PlayerMoveSpeed = 1.0f;

    [SerializeField]
    private float PlayerRotateSpeed = 60.0f;

    [SerializeField]
    private float JumpForce = 5.0f;

    private CapsuleCollider _playerCollider = null;
    private Rigidbody _playerRigidbody = null;

    private float _playerVerticalRotate = 0.0f;
    private float _playerHorizontalRotate = 0.0f;

    #region MonoBehaviour Callbacks
    private void Start()
    {
        _playerCollider = GetComponent<CapsuleCollider>();
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
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) JumpOnPosition();
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = GetNextDirectionByKeyInput() * Time.fixedDeltaTime * PlayerMoveSpeed
                            + _playerRigidbody.position;
        _playerRigidbody.MovePosition(nextPosition);
    }

    private void LateUpdate()
    {
        SetPlayerRotationByMouseInput();
    }
    #endregion

    #region Private Field
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

        Vector3 playerEulerAngle = PlayerCamera.transform.rotation.eulerAngles;
        playerEulerAngle.x = _playerVerticalRotate;
        playerEulerAngle.y = _playerHorizontalRotate;

        PlayerCamera.transform.rotation = Quaternion.Euler(playerEulerAngle);
    }

    private void JumpOnPosition()
    {
        _playerRigidbody.AddForce(transform.up * _playerRigidbody.mass * JumpForce, ForceMode.Impulse);
    }
    #endregion
}
