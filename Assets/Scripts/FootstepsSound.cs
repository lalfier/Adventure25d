using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public void PlayFootstepsSound()
    {
        //Trigger sound from animation event
        AudioManager.PlayFootstepAudio();
    }
}
