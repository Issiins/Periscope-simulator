using UnityEngine;

namespace Core.Scripts
{
    /// <summary>
    /// Used for following object's position and rotation  
    /// </summary>
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private Transform target;
        void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
