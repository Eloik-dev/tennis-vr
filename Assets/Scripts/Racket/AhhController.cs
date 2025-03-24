using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AhhController : MonoBehaviour
{
    [SerializeField]
    AudioSource aahhhSource;

    Rigidbody rb;

    public float minSpeed = 0.1f;
    public float maxSpeed = 10f;

    public float conditionSpeed = 1f;
    public float stopDelay = 0.5f;

    private float timeBelowThreshold = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float velocityMagnitude = rb.linearVelocity.magnitude;

        if (velocityMagnitude <= conditionSpeed)
        {
            timeBelowThreshold += Time.deltaTime;

            if (timeBelowThreshold >= stopDelay)
            {
                if (aahhhSource.isPlaying)
                {
                    aahhhSource.Stop();
                }
            }
        }
        else
        {
            timeBelowThreshold = 0f;

            float volume = Mathf.Clamp(velocityMagnitude / 10f, 0f, 1f);
            aahhhSource.volume = volume;

            AdjustSoundDurationBasedOnSpeed(velocityMagnitude);

            if (!aahhhSource.isPlaying)
            {
                aahhhSource.Play();
            }
        }
    }

    /// <summary>
    /// Ajuste le son en se basant sur la vélocité de la raquette
    /// </summary>
    /// <param name="velocity"></param>
    void AdjustSoundDurationBasedOnSpeed(float velocity)
    {
        float pitch = Mathf.Lerp(1f, 2f, Mathf.InverseLerp(minSpeed, maxSpeed, velocity));

        aahhhSource.pitch = pitch;
    }
}
