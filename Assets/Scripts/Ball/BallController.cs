using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(AudioSource))]
public class BallController : MonoBehaviour
{
    [SerializeField] LevelState levelState;
    [SerializeField] Transform ballSpawnPoint;

    [SerializeField] float directionThrowBackMultiplier = 15f;
    [SerializeField] float upThrowBackMultiplier = 2f;

    private AudioSource audioSource;
    private InfiniteBounce infiniteBounce;

    public float maxSpeed = 10f;
    private Rigidbody rb;

    private bool isAtStartingPosition = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        infiniteBounce = GetComponent<InfiniteBounce>();
    }

    private void Update()
    {
        if (!levelState.IsLevelStarted && !isAtStartingPosition)
        {
            Debug.Log("Moving to initial position");

            rb.MovePosition(ballSpawnPoint.position);
            rb.MoveRotation(ballSpawnPoint.rotation);

            // Clear velocity and angular velocity
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            isAtStartingPosition = true;
        }
        else if (levelState.IsLevelStarted)
        {
            isAtStartingPosition = false;
        }

        infiniteBounce.enabled = isAtStartingPosition;

        if (levelState.ShouldBallComeBack)
        {
            ThrowBallBack();
        }
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayPufferfish();

        if (collision.collider.CompareTag("Racket"))
        {
            levelState.StartLevel();
        }
    }

    /// <summary>
    /// Joue le son lorsque la balle touche le sol ou la raquette
    /// </summary>
    private void PlayPufferfish()
    {
        audioSource.Play();
    }

    /// <summary>
    /// Relance la balle vers le joueur à l'aide de physique
    /// </summary>
    private void ThrowBallBack()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        Vector3 force = direction * directionThrowBackMultiplier + Vector3.up * upThrowBackMultiplier;
        rb.AddForce(force, ForceMode.Impulse);

        levelState.NotifyBallCameBack();
    }
}
