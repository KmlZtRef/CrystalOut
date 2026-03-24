using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// The utility class that contains extension method for vector 
    /// </summary>
    public static class Mathv
    {
        #region RoundVectorToInt
        /// <summary>
        /// Rounds all value in vector to int.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>result</returns>
        public static Vector3 RoundVectorToInt(this Vector3 value)
        => new(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y), Mathf.RoundToInt(value.z));
        #endregion
        
        #region FloorVectorToInt
        /// <summary>
        /// Floor all value in vector to int.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>result</returns>
        public static Vector3 FloorVectorToInt(this Vector3 value)
        => new(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y), Mathf.FloorToInt(value.z));
        #endregion
        
        #region CeilVectorToInt
        /// <summary>
        /// Ceil all value in vector to int.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>result</returns>
        public static Vector3 CeilVectorToInt(this Vector3 value)
        => new(Mathf.CeilToInt(value.x), Mathf.CeilToInt(value.y), Mathf.CeilToInt(value.z));
        #endregion
        
        #region VectorToVectorInt
        /// <summary>
        /// Rounds all value in vector to int and return as VectorInt.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>result</returns>
        public static Vector3Int VectorToVectorInt(this Vector3 value)
        => new(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y), Mathf.RoundToInt(value.z));
        #endregion
        
        #region Distance
        /// <summary>
        /// Gets distance to target point.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="point">target point</param>
        /// <returns>result</returns>
        public static float Distance(this Vector3 value, Vector3 point)
        => Vector3.Distance(value, point);
        #endregion
        
        #region RotationFromVectorDeg
        /// <summary>
        /// Get rotation(Degrees) from direction vector. This method is for Vector2.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static float RotationFromVectorDeg(Vector2 direction)
        => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        #endregion
        
        #region RotationFromVectorRad
        /// <summary>
        /// Get rotation(Radian) from direction vector. This method is for Vector2.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static float RotationFromVectorRad(Vector2 direction)
        => Mathf.Atan2(direction.y, direction.x);
        #endregion
        
        #region ArrivedAt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="target"></param>
        /// <param name="minDistance"></param>
        /// <returns></returns>
        public static bool ArrivedAt(this Vector3 position, Vector3 target, float minDistance = 0.005f)
        => position.Distance(target) < minDistance;
        #endregion
    }
}
