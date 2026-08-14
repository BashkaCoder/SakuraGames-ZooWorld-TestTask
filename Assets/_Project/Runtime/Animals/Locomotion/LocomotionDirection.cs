using UnityEngine;

namespace ZooWorld.Animals.Locomotion
{
    public static class LocomotionDirection
    {
        private const float FullTurnRadians = Mathf.PI * 2f;

        public static Vector3 RandomHorizontal(IRandomSource random)
        {
            var angle = random.Value01() * FullTurnRadians;
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }
    }
}
