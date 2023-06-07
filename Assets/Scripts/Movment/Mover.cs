using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float initialSpeed = 0.3f;

    [SerializeField]
    private float accelerationRate = 0.009f;

    [SerializeField]
    private float maxSpeed = 2f;

    [SerializeField]
    private float hitMovementDistance = 0.3f;

    private Rigidbody2D rb;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
    }

    private void FixedUpdate()
    {
        // Calculate direction from the ship to the player
        Vector3 direction = playerTransform.position - transform.position;

        // Normalize the direction vector
        direction.Normalize();

        // Move the ship in the direction with the current speed
        rb.velocity = direction * currentSpeed;

        // Gradually increase the speed up to the max speed
        currentSpeed += accelerationRate * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, maxSpeed);
    }

    public void MoveUp()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();
        transform.position -= direction * hitMovementDistance;
    }
}