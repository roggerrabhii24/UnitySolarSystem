using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    public AudioSource engineSound;

    public float minPitch = 0.8f;
    public float maxPitch = 1.6f;
    public float maxVolume = 0.8f;

    void Start()
    {
        engineSound.loop = true;
        engineSound.volume = 0f;
        engineSound.Stop();
    }

    void Update()
    {
        float forwardInput = Mathf.Abs(Input.GetAxis("Vertical"));
        bool verticalInput = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E);

        float movementAmount = forwardInput;

        if (verticalInput)
            movementAmount = Mathf.Max(movementAmount, 0.6f);

        if (movementAmount > 0.01f)
        {
            if (!engineSound.isPlaying)
                engineSound.Play();

            engineSound.volume = Mathf.Lerp(
                engineSound.volume,
                maxVolume * movementAmount,
                Time.deltaTime * 5f
            );

            engineSound.pitch = Mathf.Lerp(
                engineSound.pitch,
                Mathf.Lerp(minPitch, maxPitch, movementAmount),
                Time.deltaTime * 5f
            );
        }
        else
        {
            engineSound.volume = Mathf.Lerp(
                engineSound.volume,
                0f,
                Time.deltaTime * 5f
            );

            if (engineSound.volume < 0.01f && engineSound.isPlaying)
                engineSound.Stop();
        }
    }
}