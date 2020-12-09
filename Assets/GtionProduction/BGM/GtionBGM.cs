using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GtionProduction
{

    [RequireComponent(typeof(AudioSource))]
    public class GtionBGM : MonoBehaviour
    {
        static GtionBGM _bgm;
        public static GtionBGM bgm
        {
            get
            {
                if (_bgm == null)
                {
                    GameObject prefab = Resources.Load<GameObject>("General/BGM");

                    GameObject temp = Instantiate(prefab);
                    _bgm = temp.GetComponent<GtionBGM>();
                    _bgm.audioSource = temp.GetComponent<AudioSource>();

                    DontDestroyOnLoad(_bgm.gameObject);
                    //return _bgm;
                }
                return _bgm;
            }
            set
            {
                _bgm = value;
                //DontDestroyOnLoad(_loading.gameObject);
            }
        }

        const float Smooth = 2;

        AudioSource audioSource = null;
        AudioClip nextClip = null;

        float maxVolume = 0.3f;
        bool isStopped = false;

        float target = 0;
        float velo = 0;

        public static void Play(AudioClip clip, float maxVolume = 0.3f, bool replayed = false, bool loop = true)
        {
            if (replayed || bgm.audioSource.clip != clip)
            {

                // jika yang akan di play sama
                bgm.maxVolume = maxVolume;
                bgm.nextClip = clip;                
                bgm.target = 0;
                bgm.audioSource.loop = loop;

            }
            bgm.isStopped = false;
        }
        public static void Play(string clipName, float maxVolume = 0.3f, bool replayed = false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
            Play(clip, maxVolume, replayed);
        }

        public static void Stop()
        {
            bgm.isStopped = true;
            bgm.target = 0;
        }

        public static void Pause()
        {
            bgm.audioSource.Pause();
        }

        public static void Resume()
        {
            bgm.audioSource.UnPause();
        }

        public static void Mute(bool isMute)
        {
            bgm.audioSource.mute = isMute;
        }

        // Update is called once per frame
        void Update()
        {
            if (target == 0)
            {
                audioSource.volume = Mathf.SmoothDamp(audioSource.volume, target, ref velo, Smooth / 2);
            }
            else
            {
                audioSource.volume = Mathf.SmoothDamp(audioSource.volume, target, ref velo, Smooth);
            }

            if (audioSource.volume < 0.03f && !isStopped)
            {
                target = maxVolume;
                audioSource.Stop();
                audioSource.clip = nextClip;
                audioSource.Play();
            }

        }
    }
}
