using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    [Header("Spaceship Settings")]
    [SerializeField] private float accelerationFactor = 5.0f;
    [SerializeField] private float turnFactor = 3.5f;

    // Local variables
    float throttleInput = 0.0f;
    float steeringInput = 0.0f;
    Quaternion rotationAngle = Quaternion.identity;

    // Components
    Rigidbody spaceshipRigidbody;

    // These variables are to hold the Action references
    InputAction throttleAction;
    InputAction steeringAction;

    private void Awake()
    {
        spaceshipRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Find the references to the "Move" and "Look" actions
        throttleAction = InputSystem.actions.FindAction("Throttle");
        steeringAction = InputSystem.actions.FindAction("Steering");
    }

    void Update()
    {
        throttleInput = throttleAction.ReadValue<float>();
        steeringInput = steeringAction.ReadValue<float>();   
    }

    void FixedUpdate()
    {
        ApplyThrottle();
        ApplySteering();        
    }

    void ApplyThrottle()
    {
        Vector3 forwardForce = throttleInput * accelerationFactor * transform.forward;
        spaceshipRigidbody.AddForce(forwardForce, ForceMode.Acceleration);
    }

    void ApplySteering()
    {
        rotationAngle *= Quaternion.AngleAxis(steeringInput * turnFactor, Vector3.up);
        spaceshipRigidbody.MoveRotation(rotationAngle);
    }
}
