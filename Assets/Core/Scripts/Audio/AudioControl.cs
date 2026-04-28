using Core.Scripts.Control_Table.Control;
using UnityEngine;

namespace Code.Scripts.Audio
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] private RotationChecker rotationChecker;
        private AudioSource _audioSource;
        private float offset = 0.1f;
        private bool _isPlaying = false;
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.mute = true;
        }

        void Update()
        {
            var currentValue = Mathf.Clamp(Mathf.Abs(rotationChecker.RotationQuotient), 0f, 0.9f)*0.5f;
            switch (_isPlaying)
            {
                case true:
                    _audioSource.volume = currentValue;
                    if (currentValue <= offset)
                    {
                        _isPlaying = false;
                        _audioSource.mute = true;
                        _audioSource.Stop();
                    }
                    break;
                case false when currentValue > offset:
                    _isPlaying = true;
                    _audioSource.mute = false;
                    _audioSource.Play();
                    break;
            }
        }
    }
}
