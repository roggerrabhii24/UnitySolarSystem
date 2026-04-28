using UnityEngine;

public class RobotSpeakerToggle : MonoBehaviour
{
    public AudioSource robotVoice;

    public void ToggleInformation()
    {
        if (robotVoice != null)
        {
            if (robotVoice.isPlaying)
            {
                robotVoice.Stop();
            }
            else
            {
                robotVoice.Play();
            }
        }
    }
}