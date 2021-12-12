using System.Collections.Generic;
using UnityEngine;

public class OrbitOptimized : MonoBehaviour
{
    [SerializeField] private Vector2 m_RadiusRange = new Vector2(5f, 10f);
    [SerializeField] private Vector2 m_SpeedRange = new Vector2(0.5f, 1f);
    [SerializeField] private int m_Count = 10;
    [SerializeField] private Mesh m_Mesh;
    [SerializeField] private Material m_Material;

    private List<AsteroidDescription> m_Asteroids;
    private List<Matrix4x4[]> m_Batches;
    private float m_Time;

    private struct AsteroidDescription
    {
        public Quaternion Rotation;
        public float Radius;
        public float Angle0;
        public float Speed;
        public int BatchIndex;
        public int MatrixIndex;
    }

    void Start()
    {
        // Instance all the objects
        m_Asteroids = new List<AsteroidDescription>();
        
        var batchCount = Mathf.CeilToInt(m_Count / 1023f);
        m_Batches = new List<Matrix4x4[]>(batchCount);

        for (int i = 0; i < batchCount; i++)
        {
            m_Batches.Add(new Matrix4x4[1023]);
        }

        for (int i = 0; i < m_Count; i++)
        {
            var batchIndex = Mathf.FloorToInt(i / 1023);
            var MatrixIndex = i % 1023;

            var asteroidDescription = new AsteroidDescription
            {
                Rotation = Random.rotation,
                Radius = Random.value,
                Angle0 = Random.Range(0, 2 * Mathf.PI),
                Speed = Random.value,
                BatchIndex = batchIndex,
                MatrixIndex = MatrixIndex,
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
            var radius = Mathf.Lerp(m_RadiusRange.x, m_RadiusRange.y, asteroidDescription.Radius);
            var position = new Vector3();

            position.x = radius * Mathf.Cos(angle);
            position.y = 0;
            position.z = radius * Mathf.Sin(angle);

            var matrix = Matrix4x4.TRS(position, asteroidDescription.Rotation, Vector3.one);
            m_Batches[asteroidDescription.BatchIndex][asteroidDescription.MatrixIndex] = matrix;
        }

        for (int i = 0; i < m_Batches.Count - 1; i++)
        {
            Graphics.DrawMeshInstanced(m_Mesh, 0, m_Material, m_Batches[i], 1023);
        }

        Graphics.DrawMeshInstanced(m_Mesh, 0, m_Material, m_Batches[m_Batches.Count - 1], m_Count % 1023);
    }

    void OnDrawGizmosSelected()
    {
        // // Display the explosion radius when selected
        // Gizmos.color = new Color(1, 1, 0, 0.75F);
        // Gizmos.DrawSphere(transform.position, explosionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_RadiusRange.x);
        Gizmos.DrawWireSphere(transform.position, m_RadiusRange.y);
    }
}
