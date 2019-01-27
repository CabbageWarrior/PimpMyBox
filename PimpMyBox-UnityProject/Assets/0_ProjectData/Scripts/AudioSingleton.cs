using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public enum Sound
    {
        MenuButton,

        Dash,
        PlayerWallBounce,
        PVPBounce,

        PickupObject,
        DropObject,

        SingleObjTrash,
        MultipleObjTrash,

    }

    static AudioSingleton instance;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public static AudioSingleton Instance
    {
        get
        {
            if (instance == null) instance = new AudioSingleton();
            return instance;
        }
    }

    public AudioSource menuBtn;
    public AudioSource dash;
    public AudioSource playerWallBounce;
    public AudioSource pvpBounce;
    public AudioSource pickupObj;
    public AudioSource dropObj;
    public AudioSource singleObjTrash;
    public AudioSource multipleObjTrash;
    


    public static void PlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.MenuButton:
                Instance.menuBtn.Play();
                break;
            case Sound.Dash:
                Instance.dash.Play();
                break;
            case Sound.PlayerWallBounce:
                Instance.playerWallBounce.Play();
                break;
            case Sound.PVPBounce:
                Instance.pvpBounce.Play();
                break;
            case Sound.PickupObject:
                Instance.pickupObj.Play();
                break;
            case Sound.DropObject:
                Instance.dropObj.Play();
                break;
            case Sound.SingleObjTrash:
                Instance.singleObjTrash.Play();
                break;
            case Sound.MultipleObjTrash:
                Instance.multipleObjTrash.Play();
                break;
            default:
                break;
        }
    }
}
