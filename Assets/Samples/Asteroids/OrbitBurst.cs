using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using Unity.Collections;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;

public class OrbitBurst : MonoBehaviour
{
    [SerializeField] private Vector2 m_RadiusRange = new Vector2(5f, 10f);
    [SerializeField] private Vector2 m_SpeedRange = new Vector2(0.5f, 1f);
    [SerializeField] private int m_Count = 10;
    [SerializeField] private Mesh m_Mesh;
    [SerializeField] private Material m_Material;

    private NativeArray<AsteroidDescription> m_Asteroids;
    private NativeArray<float4x4> m_Matrices;
    private Matrix4x4[] m_Batch;
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
        m_Asteroids = new NativeArray<AsteroidDescription>(m_Count, Allocator.Persistent);
        m_Matrices = new NativeArray<float4x4>(m_Count, Allocator.Persistent);
        m_Batch = new Matrix4x4[1023];

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

            m_Asteroids[i] = asteroidDescription;
        }
    }

    void OnDestroy()
    {
        m_Asteroids.Dispose();
        m_Matrices.Dispose();
    }

    unsafe void Update()
    {
        // Move objects
        m_Time += Time.deltaTime;

        var updateJob = new UpdateJob
        {
            Asteroids = m_Asteroids,
            Matrices = m_Matrices,
            SpeedRange = m_SpeedRange,
            RadiusRange = m_RadiusRange,
            Time = m_Time,
        };

        var jobHandle = updateJob.Schedule(m_Asteroids.Length, Mathf.CeilToInt(m_Asteroids.Length / 8));
        jobHandle.Complete();

        var bachCount = Mathf.CeilToInt(m_Count / 1023f);

        for (int i = 0; i < bachCount - 1; i++)
        {
            var matrixCount = 1023;
            if (i == bachCount - 1)
            {
                matrixCount = m_Count % 1023;
            }
            // m_Matrices.GetSubArray(i * 1023, matrixCount).Reinterpret<Matrix4x4>().CopyTo(m_Batch);

            fixed (void* batchPtr = &m_Batch[0])
            {
                var matricesPtr = (float4x4*) m_Matrices.GetUnsafePtr();
                UnsafeUtility.MemCpy(batchPtr, matricesPtr + i * 1023, matrixCount * UnsafeUtility.SizeOf<float4x4>());
            }

            Graphics.DrawMeshInstanced(m_Mesh, 0, m_Material, m_Batch, matrixCount);
        }
    }

    [BurstCompile]
    struct UpdateJob : IJobParallelFor
    {
        public NativeArray<AsteroidDescription> Asteroids;
        public NativeArray<float4x4> Matrices;
        public float2 SpeedRange;
        public float2 RadiusRange;
        public float Time;

        public void Execute(int i)
        {  
            var asteroidDescription = Asteroids[i];
            var speed = math.lerp(SpeedRange.x, SpeedRange.y, asteroidDescription.Speed);
            var angle = speed * Time + asteroidDescription.Angle0;
            var radius = math.lerp(RadiusRange.x, RadiusRange.y, asteroidDescription.Radius);
            var position = new Vector3();

            position.x = radius * math.cos(angle);
            position.y = math.sin(angle * 10) * 10 * asteroidDescription.Radius;
            position.z = radius * math.sin(angle);

            var matrix = float4x4.TRS(position, asteroidDescription.Rotation, Vector3.one);
            Matrices[i] = matrix;
        }
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
