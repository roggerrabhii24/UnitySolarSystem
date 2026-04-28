using UnityEngine;

public class MarsRobotSpeakerToggle : MonoBehaviour
{
    public AudioSource robotVoice;

    public void ToggleInformation()
    {
        if (robotVoice == null) return;

        if (robotVoice.isPlaying)
            robotVoice.Stop();
        else
            robotVoice.Play();
    }
}