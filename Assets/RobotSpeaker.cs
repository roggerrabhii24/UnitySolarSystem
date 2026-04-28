using UnityEngine;

public class RobotSpeakerAnimation : MonoBehaviour
{
    public AudioSource robotVoice;

    public void ToggleInformation()
    {
        if (robotVoice != null)
        {
            // If already playing → stop
            if (robotVoice.isPlaying)
            {
                robotVoice.Stop();
            }
            else
            {
                // Otherwise play
                robotVoice.Play();
            }
        }
    }
}