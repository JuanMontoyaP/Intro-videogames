using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitGameObject : MonoBehaviour
{
    [SerializeField] private float m_RadiusMin = 5;
    [SerializeField] private float m_RadiusMax = 10;
    [SerializeField] private float m_SpeedMin = 0.5f;
    [SerializeField] private float m_SpeedMax = 1.0f;
    [SerializeField] private int m_Count = 10;
    [SerializeField] private Mesh m_Mesh;
    [SerializeField] private Material m_Material;

    private List<AsteroidDescription> m_Asteroids;
    private float m_Time;
    private struct AsteroidDescription
    {
        public GameObject AsteroidGO;
        public float Radius;
        public float Angle0;
        public float Speed;
    }

    void Start()
    {
        // Instance all objects
        m_Asteroids = new List<AsteroidDescription>();

        for (int i = 0; i < m_Count; i++)
        {
            var asteroid = new GameObject();

            var asteroidTransform = asteroid.GetComponent<Transform>();
            asteroidTransform.SetParent(transform);

            var meshFilter = asteroid.AddComponent<MeshFilter>();
            meshFilter.mesh = m_Mesh;

            var meshRenderer = asteroid.AddComponent<MeshRenderer>();
            meshRenderer.material = m_Material;

            var asteroidDescription = new AsteroidDescription
            {
                AsteroidGO = asteroid,
                Radius = Random.Range(m_RadiusMin, m_RadiusMax),
                Angle0 = Random.Range(0, 2 * Mathf.PI),
                Speed = Random.Range(m_SpeedMin, m_SpeedMin),
            };

            m_Asteroids.Add(asteroidDescription);
        }
    }

    void Update()
    {
        // Move objects
        m_Time += Time.deltaTime;
        for (int i = 0; i < m_Asteroids.Count; i++)
        {
            var asteroidDescription = m_Asteroids[i];
            var angle = asteroidDescription.Speed * m_Time + asteroidDescription.Angle0;
            var position = new Vector3();
            position.x = asteroidDescription.Radius * Mathf.Cos(angle);
            position.y = 0;
            position.z = asteroidDescription.Radius * Mathf.Sin(angle);
            asteroidDescription.AsteroidGO.transform.position = position;
        }
    }
}
