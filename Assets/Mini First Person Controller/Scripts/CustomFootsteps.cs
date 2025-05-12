using UnityEngine;

public class CustomFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip grassStep;
    public AudioClip sandStep;
    public AudioClip woodStep;

    public float minMoveSpeed = 0.1f;
    private string currentSurface = "";
    private Rigidbody rb;

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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            string tag = hit.collider.tag;

            if (tag != currentSurface)
            {
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
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
