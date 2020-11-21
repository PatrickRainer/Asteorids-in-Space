using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameManagers
{
    public class MusicController : MonoBehaviour
    {
        [InlineButton(nameof(MoveNextClip))] [SerializeField]
        List<AudioClip> clips = new List<AudioClip>();

        AudioSource _audioSource;
        int _currentIndex;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = clips[_currentIndex];
            _audioSource.Play();
        }

        void Update()
        {
            if (!_audioSource.isPlaying) MoveNextClip();
        }


        void MoveNextClip()
        {
            var maxIndex = clips.Count-1;
            if (_currentIndex == maxIndex)
            {
                _currentIndex = 0;
                _audioSource.clip = clips[_currentIndex];
                _audioSource.Play();
            }
            else
            {
                _currentIndex += 1;
                _audioSource.clip = clips[_currentIndex];
                _audioSource.Play();
            }
        }
    }
}