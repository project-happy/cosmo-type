using UnityEngine;
using UnityEngine.Experimental.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    public Transform playerTransform;

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

    private Vector3 initialTarget;

    private bool isSnappedToTragget = false;

    private Vector3 currentTarget;

    public float Dist() => Vector3.Distance(transform.position, playerTransform.position);

    public float DistFromCenter() => Mathf.Abs(Camera.main.transform.position.y - transform.position.y);

    private void OnDisable()
    {
        rb.velocity = new Vector3();
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
        initialTarget = new Vector3(
            Random.Range(Camera.main.transform.position.x - 8, Camera.main.transform.position.x + 8),
            Random.Range(transform.position.y, Camera.main.transform.position.y));
        currentTarget = playerTransform.position;
    }

    private void FixedUpdate()
    {

        

        // Calculate direction from the ship to the player
        Vector3 direction = currentTarget - transform.position;

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