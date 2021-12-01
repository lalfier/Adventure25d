using UnityEngine;

public class ChangeSurfaceAudio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Change audio surface on trigger enter
        AudioManager.ChangeSurface(SurfaceFootsteps.Wood);
    }

    private void OnTriggerExit(Collider other)
    {
        //Restore audio surface on trigger exit
        AudioManager.ChangeSurface(SurfaceFootsteps.Default);
    }
}
