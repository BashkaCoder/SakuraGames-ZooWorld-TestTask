using UnityEngine;

namespace ZooWorld.Animals
{
    public class UnityRandomSource : IRandomSource
    {
        public float Value01()
        {
            return Random.value;
        }

        public bool NextBool()
        {
            return Random.value < 0.5f;
        }
    }
}
