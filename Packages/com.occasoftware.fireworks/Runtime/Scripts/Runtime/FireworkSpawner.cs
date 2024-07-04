using System.Collections.Generic;

using UnityEngine;

namespace OccaSoftware.Fireworks.Runtime
{
    public class FireworkSpawner : MonoBehaviour
    {
        public List<GameObject> visualEffects = new List<GameObject>();

        public float spawnRadius = 20f;
        public float spawnRate = 2f;

        float randomizedRate;
        float timeTracker;

        void Start()
        {
            timeTracker = Time.time;
            randomizedRate = spawnRate * Random.Range(1f, 2f);
        }

        void Update()
        {
            if (Time.time - timeTracker > randomizedRate)
            {
                Spawn();
                timeTracker = Time.time;
                randomizedRate = spawnRate * Random.Range(1f, 2f);
            }
        }

        void Spawn()
        {
            GameObject go = visualEffects[Random.Range(0, visualEffects.Count)];
            Vector3 sphere = Random.insideUnitSphere * spawnRadius;
            Instantiate(go, new Vector3(sphere.x, 0, sphere.y), Quaternion.identity);
        }
    }
}
