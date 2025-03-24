using UnityEngine;

public class RacketController : MonoBehaviour
{
    [SerializeField] Transform controller;
    [SerializeField] Material redRacketMaterial;
    [SerializeField] Material blueRacketMaterial;
    [SerializeField] bool isBlueRacket = false;
    [SerializeField] MeshRenderer bodyRenderer;

    Rigidbody rb;

    public float positionSmoothTime = 0.1f;
    public float rotationSmoothTime = 0.1f;

    private Vector3 positionVelocity;
    private Quaternion targetRotation;
    private float rotationVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        updateMaterial();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = controller.position;
        rb.MovePosition(Vector3.SmoothDamp(rb.position, targetPosition, ref positionVelocity, positionSmoothTime));

        targetRotation = controller.rotation;
        float angle;
        Vector3 axis;
        (targetRotation * Quaternion.Inverse(rb.rotation)).ToAngleAxis(out angle, out axis);
        float deltaRotation = Mathf.SmoothDampAngle(angle, 0, ref rotationVelocity, rotationSmoothTime);
        rb.MoveRotation(Quaternion.AngleAxis(deltaRotation, axis) * rb.rotation);
    }

    /// <summary>
    /// Met à jour les matériaux de la raquette selon la couleur sélectionnée
    /// </summary>
    private void updateMaterial()
    {
        if (!bodyRenderer) return;

        Material[] materials = bodyRenderer.materials;

        if (materials.Length > 1)
        {
            materials[1] = isBlueRacket ? blueRacketMaterial : redRacketMaterial;
        }

        bodyRenderer.materials = materials;
    }
}
