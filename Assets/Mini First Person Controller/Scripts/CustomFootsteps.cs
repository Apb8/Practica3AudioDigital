using UnityEngine;

public class CustomFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip grassStep;
    public AudioClip sandStep;
    public AudioClip woodStep;

    public float minMoveSpeed = 0.1f;
    public float surfaceChangeCooldown = 0.5f;

    private string currentSurface = "";
    private Rigidbody rb;
    private float lastSurfaceChangeTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource.loop = true;
    }

    void Update()
    {
        bool isMoving = IsGrounded() && rb.velocity.magnitude > minMoveSpeed;

        if (isMoving)
        {
            UpdateSurfaceAndAudio();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                currentSurface = "";
            }
        }
    }

    void UpdateSurfaceAndAudio()
    {
        RaycastHit hit;

        // cambio raycast q sino no va bn
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 2f))
        {
            string tag = hit.collider.tag;

            // evitar cambios frecuentes
            if (tag != currentSurface && Time.time - lastSurfaceChangeTime > surfaceChangeCooldown)
            {
                lastSurfaceChangeTime = Time.time;
                currentSurface = tag;

                switch (tag)
                {
                    case "grass":
                        audioSource.clip = grassStep;
                        break;
                    case "ground":
                        audioSource.clip = sandStep;
                        break;
                    case "wood":
                        audioSource.clip = woodStep;
                        break;
                    default:
                        audioSource.clip = null;
                        break;
                }

                if (audioSource.clip != null)
                {
                    audioSource.Play();
                }
            }
            else if (!audioSource.isPlaying && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.1f);
    }
}
