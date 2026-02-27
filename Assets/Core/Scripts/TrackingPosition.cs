using UnityEngine;

namespace Core.Scripts
{
    public static class TrackingPosition 
    {
        /// <summary>
        /// Calculates the mid-point of two-dimensional line (X and Z axes) 
        /// </summary>
        /// <param name="leftPoint">first point</param>
        /// <param name="rightPoint">second point</param>
        /// <returns>mid-point the line</returns>
        public static Vector3 CalculateMiddlePoints(Transform leftPoint, Transform rightPoint)
        {
            var midPoint = new Vector3((leftPoint.position.x + rightPoint.position.x) / 2,
                Vector3.zero.y,
                (leftPoint.position.z + rightPoint.position.z) / 2);
            return midPoint;
        }
    }
}
