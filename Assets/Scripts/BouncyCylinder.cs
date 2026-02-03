using UnityEngine;

public class BouncyCylinder : MonoBehaviour
{
    public float bounce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;

        if (rb != null && rb.CompareTag("Player"))
        {
            // direction from collision to cylinder
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            // clear the original velocity
            rb.linearVelocity = Vector3.zero;
            // add a force in opposite dir
            rb.AddForce(dir * bounce, ForceMode.Impulse);
        }
    }
}
