using UnityEngine;

public class MoonRobotSpeakerToggle : MonoBehaviour
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