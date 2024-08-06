using FMODUnityResonance;
using UnityEngine;

public enum Bgm {
    Space, Aurora, Cloudy, Mesosphere, Sunset, Earth
}

public class BGMManagerScript : MonoBehaviour
{
    // SINGLETON
    public static BGMManagerScript S;

    public FMOD.Studio.EventInstance cloudyBGMInstance, auroraBGMInstance, SpaceBGMInstance, MesosphereBGMInstance, SunsetBGMInstance, EarthBGMInstance, currentlyPlayingSong, nullsong;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (S != null) {Destroy(gameObject);}
        else {S = this;}
    }

    // Start is called before the first frame update
    void Start()
    {
        cloudyBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/CloudyBGM");
        auroraBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/AuroraBGM");
        SpaceBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/SpaceBGM");
        MesosphereBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/MesosphereBGM");
        SunsetBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/SunsetBGM");
        EarthBGMInstance = FMODUnity.RuntimeManager.CreateInstance("event:/BGMs/EarthBGM");
        //PlayBGM(Bgm.Cloudy);
    }

    // Update is called once per frame
    void Update()
    {
        // why is this here
        // if (Input.GetKeyDown(KeyCode.W)) {StopBGM(Bgm.Cloudy); }
        
    }

    public void PlayBGM(Bgm bgm) {
        if (bgm == Bgm.Cloudy && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(cloudyBGMInstance))
        {
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = cloudyBGMInstance;
            cloudyBGMInstance.start();
        }
        else if (bgm == Bgm.Aurora && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(auroraBGMInstance)){
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = auroraBGMInstance;
            auroraBGMInstance.start();
        }
        else if (bgm == Bgm.Space && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(SpaceBGMInstance)){
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = SpaceBGMInstance;
            SpaceBGMInstance.start();
        }
        else if (bgm == Bgm.Mesosphere && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(MesosphereBGMInstance)){
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = MesosphereBGMInstance;
            MesosphereBGMInstance.start();
        }
        else if (bgm == Bgm.Sunset && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(SunsetBGMInstance)){
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = SunsetBGMInstance;
            SunsetBGMInstance.start();
        }
        else if (bgm == Bgm.Earth && GetInstantiatedEventName(currentlyPlayingSong) != GetInstantiatedEventName(EarthBGMInstance)){
            currentlyPlayingSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentlyPlayingSong = EarthBGMInstance;
            EarthBGMInstance.start();
        }
    }

    public void StopBGM(Bgm bgm) {
        if (bgm == Bgm.Cloudy) {
            cloudyBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (bgm == Bgm.Aurora) {
            auroraBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (bgm == Bgm.Space) {
            SpaceBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (bgm == Bgm.Mesosphere) {MesosphereBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);}
        else if (bgm == Bgm.Sunset) {SunsetBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);}
        else if (bgm == Bgm.Earth) {EarthBGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);}

        // currentlyPlayingSong = nullsong;
    }
    
    public FMOD.Studio.EventInstance GetSongType(Bgm bgm) {
        if (bgm == Bgm.Cloudy)
        {
            return cloudyBGMInstance;
        }
        else if (bgm == Bgm.Aurora)
        {
            return auroraBGMInstance;
        }
        else if (bgm == Bgm.Space)
        {
            return SpaceBGMInstance;
        }
        else if (bgm == Bgm.Mesosphere)
        {
            return MesosphereBGMInstance;
        }
        else if (bgm == Bgm.Sunset) {
            return SunsetBGMInstance;
        }
        else if (bgm == Bgm.Earth) {
            return EarthBGMInstance;
        }
        else {
            // why
            return cloudyBGMInstance;
        }
    }
    
    public string GetInstantiatedEventName(FMOD.Studio.EventInstance instance)
    {
        string result;
        FMOD.Studio.EventDescription description;

        instance.getDescription(out description);
        description.getPath(out result);

        // expect the result in the form event:/folder/sub-folder/eventName
        return result; 

    }
}
