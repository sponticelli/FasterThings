using System;
using LiteNinja.SOVariable;
using LiteNinja.Systems;
using UnityEngine;
using UnityEngine.Audio;

namespace LiteNinja.Audio
{
    // TODO Replace volumes and enabled with SOVariables
    
    [AddComponentMenu("LiteNinja/Systems/Audio System")]
    public class AudioSystem : ASystem
    {
        [SerializeField, HideInInspector] private AudioSource musicAudioSource;
        [SerializeField, HideInInspector] private AudioSource fxAudioSource;

        [SerializeField] private AudioMixerGroup musicAudioMixer;
        [SerializeField] private AudioMixerGroup fxAudioMixer;

        [Serializable]
        public class Config
        {
            public bool fxEnabled = true;
            public bool musicEnabled = true;

            public float fxVolume = 1;
            public float musicVolume = 0.5f;
        }

        [SerializeField] private BoolVar fxEnabled;
        [SerializeField] private BoolVar musicEnabled;
        [SerializeField] private FloatVar fxVolume;
        [SerializeField] private FloatVar musicVolume;
        
        

        private Config _config;


        private float FxVolume
        {
            get
            {
                fxAudioMixer.audioMixer.GetFloat("FX Volume", out var volume);
                return Math.Abs(volume / 80);
            }

            set => fxAudioMixer.audioMixer.SetFloat("FX Volume", Mathf.Log(Mathf.Lerp(0.001f, 1, value)) * 20);
        }

        private float MusicVolume
        {
            get
            {
                fxAudioMixer.audioMixer.GetFloat("Music Volume", out var volume);

                return Mathf.Abs(volume / 80);
            }

            set => fxAudioMixer.audioMixer.SetFloat("Music Volume", Mathf.Log(Mathf.Lerp(0.001f, 1, value)) * 20);
        }

        public void SetFXVolume(float volume)
        {
            FxVolume = (fxEnabled) ? volume : 0;
            _config.fxVolume = volume;
            fxVolume.Value = volume;
            SaveConfig();
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = (musicEnabled) ? volume : 0;
            _config.musicVolume = volume;
            musicVolume.Value = volume; 
            SaveConfig();
        }

        public void ToggleSound()
        {
            fxEnabled.Value = !fxEnabled.Value;
            FxVolume = fxEnabled.Value ? _config.fxVolume : 0;
            _config.fxEnabled = fxEnabled;
            SaveConfig();
        }

        public void ToggleMusic()
        {
            musicEnabled.Value = !musicEnabled.Value;
            MusicVolume = (musicEnabled.Value) ? _config.musicVolume : 0;
            _config.musicEnabled = musicEnabled;
            SaveConfig();
        }
        
        public void PlaySound(AudioClip clip)
        {
            if (fxAudioSource.enabled)
            {
                fxAudioSource.PlayOneShot(clip);
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (musicAudioSource.clip == clip) return;
            musicAudioSource.Stop();

            musicAudioSource.loop = true;
            musicAudioSource.clip = clip;

            if (musicAudioSource.enabled)
            {
                musicAudioSource.Play();
            }
        }

        public void PlaySoundCollection(SoundCollection soundCollection)
        {
            if (soundCollection != null)
            {
                soundCollection.Play(fxAudioSource);
            }
        }

        

        protected override void OnLoadSystem()
        {
            musicAudioSource = CreateAudioSource("Music Audio Source");
            musicAudioSource.outputAudioMixerGroup = musicAudioMixer;

            fxAudioSource = CreateAudioSource("FX Audio Source");
            fxAudioSource.outputAudioMixerGroup = fxAudioMixer;

            LoadConfig();
            Debug.Log("Audio System Loaded");
        }

        protected override void OnUnloadSystem()
        {
            
        }


        private AudioSource CreateAudioSource(string name)
        {
            var newSource = new GameObject().AddComponent<AudioSource>();
            newSource.transform.SetParent(transform);
            newSource.name = name;
            return newSource;
        }

        private void LoadConfig()
        {
            //TODO Implement me - save Config to file
            //Should be load if the music and/or fx is enabled
            fxAudioSource.enabled = fxEnabled.Value;
            musicAudioSource.enabled = musicEnabled.Value;
            FxVolume = fxVolume.Value;
            MusicVolume = musicVolume.Value;
        }

        private void SaveConfig()
        {
            //TODO Implement me - load Config from file
            //Should be save if the music and/or fx is enabled
        }
    }
}