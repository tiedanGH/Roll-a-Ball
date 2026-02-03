using UnityEngine;

public class RotatingWall : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
