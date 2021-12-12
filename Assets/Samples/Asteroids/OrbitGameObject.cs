using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitGameObject : MonoBehaviour
{
    [SerializeField] private Vector2 m_RadiusRange = new Vector2(5f, 10f);
    [SerializeField] private Vector2 m_SpeedRange = new Vector2(0.5f, 1.0f);
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

            var rotation = Random.rotation;
            asteroidTransform.rotation = rotation;

            var asteroidDescription = new AsteroidDescription
            {
                AsteroidGO = asteroid,
                Radius = Random.value,
                Angle0 = Random.Range(0, 2 * Mathf.PI),
                Speed = Random.value,
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
            var speed = Mathf.Lerp(m_SpeedRange.x, m_SpeedRange.y, asteroidDescription.Speed);
            var angle = speed * m_Time + asteroidDescription.Angle0;
            var position = new Vector3();
            var radius = Mathf.Lerp(m_RadiusRange.x, m_RadiusRange.y, asteroidDescription.Radius);

            position.x = radius * Mathf.Cos(angle);
            position.y = 0;
            position.z = radius * Mathf.Sin(angle);
            asteroidDescription.AsteroidGO.transform.position = position;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_RadiusRange.x);
        Gizmos.DrawWireSphere(transform.position, m_RadiusRange.y);
    }
}
