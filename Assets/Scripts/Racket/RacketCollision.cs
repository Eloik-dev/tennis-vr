using UnityEngine;

public class RacketCollision : MonoBehaviour
{
    public float forceMultiplier = 0.1f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Sur collision de la raquette avec la balle, ajouter une force pour la lancer plus loin
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;

        Rigidbody ballRigidbody = collision.rigidbody;
        Vector3 relativeVelocity = ballRigidbody.linearVelocity - rb.linearVelocity;
        Vector3 forceDirection = relativeVelocity.normalized;

        ballRigidbody.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);
    }
}