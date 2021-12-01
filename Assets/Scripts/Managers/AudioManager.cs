using UnityEngine;
using UnityEngine.Audio;

public enum SurfaceFootsteps
{
    Default,
    Wood
}

public class AudioManager : MonoBehaviour
{
    //Access this one through its public static methods
    static AudioManager current;

    [Header("Ambient Audio")]
    public AudioClip ambientClip;		//The background ambient sound
    public AudioClip musicClip;         //The background music 

    [Header("Stings")]
    public AudioClip doorStingClip;     //The sting played on door interact

    [Header("Player")]
    public AudioClip[] woodStepClips;       //The wood surface footstep sound effects
    public AudioClip[] defaultStepClips;    //The default footstep sound effects

    [Header("Voice")]
    public AudioClip doorVoiceClip;      //The player door interact voice

    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;//The ambient mixer group
    public AudioMixerGroup musicGroup;  //The music mixer group
    public AudioMixerGroup stingGroup;  //The sting mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup;  //The voice mixer group

    AudioSource ambientSource;			//Reference to the generated ambient Audio Source
    AudioSource musicSource;            //Reference to the generated music Audio Source
    AudioSource stingSource;            //Reference to the generated sting Audio Source
    AudioSource playerSource;           //Reference to the generated player Audio Source
    AudioSource voiceSource;            //Reference to the generated voice Audio Source

    SurfaceFootsteps currentSurface = SurfaceFootsteps.Default;

    void Awake()
    {
        //If an AudioManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this. There can be only one AudioManager
            Destroy(gameObject);
            return;
        }

        //This is the current AudioManager and it should persist between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);

        //Generate the Audio Source "channels" for our game's audio
        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        ambientSource.playOnAwake = false;
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource.playOnAwake = false;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource.playOnAwake = false;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource.playOnAwake = false;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource.playOnAwake = false;

        //Assign each audio source to its respective mixer group so that it is
        //routed and controlled by the audio mixer
        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;

        //Being playing the level audio
        StartLevelAudio();
    }

    public static void StartLevelAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the clip for ambient audio, tell it to loop, and then tell it to play
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        //Set the clip for music audio, tell it to loop, and then tell it to play
        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }

    public static void ChangeSurface(SurfaceFootsteps surface)
    {
        // Set current surface
        current.currentSurface = surface;
    }

    public static void PlayFootstepAudio()
    {
        //If there is no current AudioManager or the player source is already playing
        //a clip, exit 
        if (current == null || current.playerSource.isPlaying)
            return;

        //Check surface
        if (current.currentSurface == SurfaceFootsteps.Wood)
        {
            //Pick a random footstep sound
            int index = Random.Range(0, current.woodStepClips.Length);

            //Set the footstep clip and tell the source to play
            current.playerSource.clip = current.woodStepClips[index];
            current.playerSource.Play();
        }
        else
        {
            //Pick a random crouching footstep sound
            int index = Random.Range(0, current.defaultStepClips.Length);

            //Set the footstep clip and tell the source to play
            current.playerSource.clip = current.defaultStepClips[index];
            current.playerSource.Play();
        }
    }

    public static void PlayDoorAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the orb sting clip and tell the source to play
        current.stingSource.clip = current.doorStingClip;
        current.stingSource.Play();

        //Set the orb voice clip and tell the source to play
        current.voiceSource.clip = current.doorVoiceClip;
        current.voiceSource.Play();
    }
}
