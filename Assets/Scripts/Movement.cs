using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1.0f;
    [SerializeField] float rotationThrust = 1.0f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime, ForceMode.Acceleration);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotateLeft();
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            StopRotateRight();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    void StopRotateLeft()
    {
        leftBoosterParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    void StopRotateRight()
    {
        rightBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationThrust)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}