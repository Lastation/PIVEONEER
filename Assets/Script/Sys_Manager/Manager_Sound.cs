using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Sound : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource[] Audio_Objects;

    [Header("Sound Enviorments")]
    [SerializeField] private AudioClip[] CLIP_Enviorment;

    [Header("Sound UI")]
    [SerializeField] private AudioClip[] CLIP_UI;

    [Header("Sound OBJ")]
    [SerializeField] private AudioClip[] CLIP_OBJ;

    [Header("Sound Charactor")]
    [SerializeField] private AudioClip[] CLIP_Charactor;

    [Header("Sound Background")]
    [SerializeField] private AudioClip[] CLIP_Background;

    // Use this for initialization
    public void Init()
    {

    }

    // Update is called once per frame
    public void Updated()
    {

    }

    // Play Sounds
    public void Play_Sound(SoundType _type, int _value)
    {
        switch (_type)
        {
            // Sound Enviorments
            case SoundType.Enviorment:
                Audio_Objects[(int)_type].clip = CLIP_Enviorment[_value];
                break;
            // Sound UI
            case SoundType.UI:
                Audio_Objects[(int)_type].clip = CLIP_UI[_value];
                break;
            // Sound Object
            case SoundType.Object:
                Audio_Objects[(int)_type].clip = CLIP_OBJ[_value];
                break;
            // Sound Charactor
            case SoundType.Charactor:
                Audio_Objects[(int)_type].clip = CLIP_Charactor[_value];
                break;
            // Sound MainMenu
            case SoundType.MainMenu:
                Audio_Objects[(int)_type].clip = CLIP_Background[_value];
                break;
        }

        Audio_Objects[(int)_type].Play();
    }

    public void Play_Sound_UI(Sound_UI _type)
    {
        Audio_Objects[(int)SoundType.UI].clip = CLIP_UI[(int)_type];
        Audio_Objects[(int)SoundType.UI].Play();
    }
}
