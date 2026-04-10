using Core.Scripts.Control_Table;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private RotationChecker rotationChecker; 
    private AudioSource _audioSource;
    private float _volumeValue = 0f;
    private float offset = 0.1f;
    private bool _isPlaying = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _volumeValue = _audioSource.volume;
        _audioSource.mute = true;
    }
    
    void Update()
    {
        var currentValue = Mathf.Clamp(Mathf.Abs(rotationChecker.RotationQuotient), 0f,0.9f);
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
