using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitGameObject : MonoBehaviour
{
    [SerializeField] private float m_RadiusMin = 5;
    [SerializeField] private float m_RadiusMax = 10;
    [SerializeField] private int m_Count = 10;
    [SerializeField] private Mesh m_Mesh;
    [SerializeField] private Material m_Material;

    private List<GameObject> m_Asteroids;

    void Start()
    {
        // Instance all objects
        m_Asteroids = new List<GameObject>();

        for (int i = 0; i < m_Count; i++)
        {
            var asteroid = new GameObject();

            var asteroidTransform = asteroid.GetComponent<Transform>();
            asteroidTransform.SetParent(transform);

            var meshFilter = asteroid.AddComponent<MeshFilter>();
            meshFilter.mesh = m_Mesh;

            var meshRenderer = asteroid.AddComponent<MeshRenderer>();
            meshRenderer.material = m_Material;

            m_Asteroids.Add(asteroid);
        }
    }

    void Update()
    {
        
    }
}
