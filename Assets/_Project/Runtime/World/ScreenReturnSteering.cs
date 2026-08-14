using UnityEngine;

namespace ZooWorld.World
{
    public static class ScreenReturnSteering
    {
        public static Vector3 Redirect(Vector3 position, Vector3 direction, WorldRect bounds)
        {
            var result = direction.normalized;

            if (position.x < bounds.Min.x)
            {
                result.x = Mathf.Abs(result.x);
            }
            else if (position.x > bounds.Max.x)
            {
                result.x = -Mathf.Abs(result.x);
            }

            if (position.z < bounds.Min.y)
            {
                result.z = Mathf.Abs(result.z);
            }
            else if (position.z > bounds.Max.y)
            {
                result.z = -Mathf.Abs(result.z);
            }

            result.y = 0f;
            return result.normalized;
        }
    }
}
