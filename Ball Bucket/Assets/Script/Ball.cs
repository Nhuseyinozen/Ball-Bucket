using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Game_Manager manager;
    Rigidbody rb;
    Renderer paint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        paint = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            technicalProcess();
            manager.BallEntered();

        }
        else if (other.CompareTag("Ground"))
        {
            technicalProcess();
            manager.BallNotEntered();
        }
    }

    void technicalProcess()
    {
        manager.PartcEffect(gameObject.transform.position, paint.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
