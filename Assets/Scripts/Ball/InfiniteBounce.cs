using UnityEngine;

public class InfiniteBounce : MonoBehaviour
{
    public float initialForce = 7f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        AddPulse();
    }

    /// <summary>
    /// Sur l'entrée d'une zone, relancer la balle vers le haut pour la garder en suspension
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!this.enabled || !other.CompareTag("Zone")) return;

        AddPulse();
    }

    /// <summary>
    /// Ajoute un pulse à l'aide de physique pour lancer la balle vers le haut
    /// </summary>
    private void AddPulse()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(new Vector3(0, initialForce, 0), ForceMode.Impulse);
    }
}