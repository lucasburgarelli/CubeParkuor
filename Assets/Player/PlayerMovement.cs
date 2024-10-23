using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 255, MouseSensibility = 1, MouseBaseSpeed = 15;
    public Transform TransformPlayerCamera;
    [HideInInspector] public bool OnGround;
    private Rigidbody _rigidbody;
    private float Speed = 1, MouseAxisY;

    private void CameraMove(Vector2 inputMovement)
    {
        var mouseActualSpeed = (MouseSensibility * MouseBaseSpeed) / 10;
        var rotation = new Vector3(0.0f, mouseActualSpeed * inputMovement.x, 0.0f);
        transform.Rotate(rotation);

        rotation = new Vector3(-(mouseActualSpeed * inputMovement.y), 0.0f, 0.0f);

        var isAxisYAtLimit = rotation.x + MouseAxisY > 90 || rotation.x + MouseAxisY < -90;
        if (isAxisYAtLimit) return;

        MouseAxisY += rotation.x;
        TransformPlayerCamera.Rotate(rotation);
    }

    private void Move(Vector2 moveAxis)
    {
        var adjustedSpeed = Speed * MoveSpeed;

        _rigidbody.AddForce(transform.forward * (adjustedSpeed * moveAxis.y * Time.deltaTime * 1000));

        _rigidbody.AddForce(transform.right * (adjustedSpeed * moveAxis.x * Time.deltaTime * 1000));
    }

    private void Jump()
    {
        if (!OnGround) return;
        var adjustedSpeed = Speed * MoveSpeed;
        _rigidbody.AddForce(adjustedSpeed * Time.deltaTime * 50000 * Vector3.up, ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        OnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        OnGround = false;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var moveAxis = new Vector2
        {
            x = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0),
            y = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0)
        };
        Move(moveAxis);

        var moveAxisCamera = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        CameraMove(moveAxisCamera);

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        Speed = Input.GetKey(KeyCode.RightShift) ? 2 : 1;

        if (transform.position.y < -5) transform.position = new Vector3(0, 5, 0);
    }
}
