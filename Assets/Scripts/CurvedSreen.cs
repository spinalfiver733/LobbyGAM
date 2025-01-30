using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CurvedScreen : MonoBehaviour
{
    [SerializeField, Range(0f, 2f)]
    private float curvatureRadius = 1f;

    [SerializeField, Range(2, 100)]
    private int horizontalSegments = 20;

    [SerializeField, Range(2, 100)]
    private int verticalSegments = 20;

    [SerializeField]
    private float width = 10f;

    [SerializeField]
    private float height = 6f;

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;

    private void Awake()
    {
        // Asegurarnos de que tenemos el MeshFilter
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        // Asegurarnos de que tenemos el MeshRenderer
        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        GenerateCurvedScreen();
    }

    private void GenerateCurvedScreen()
    {
        // Crear nuevo mesh si no existe
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "CurvedScreen";
        }
        else
        {
            mesh.Clear();
        }

        meshFilter.mesh = mesh;

        // Calcular vértices
        vertices = new Vector3[(horizontalSegments + 1) * (verticalSegments + 1)];
        uvs = new Vector2[vertices.Length];

        for (int i = 0; i <= verticalSegments; i++)
        {
            float v = (float)i / verticalSegments;
            float y = height * (v - 0.5f);

            for (int j = 0; j <= horizontalSegments; j++)
            {
                float u = (float)j / horizontalSegments;
                float x = width * (u - 0.5f);

                // Aplicar curvatura
                float angle = (x / width) * Mathf.PI * curvatureRadius;
                float curvedX = Mathf.Sin(angle) * width / Mathf.PI;
                float curvedZ = (1f - Mathf.Cos(angle)) * width / Mathf.PI;

                int index = i * (horizontalSegments + 1) + j;
                vertices[index] = new Vector3(curvedX, y, curvedZ);
                uvs[index] = new Vector2(u, v);
            }
        }

        // Generar triángulos
        triangles = new int[horizontalSegments * verticalSegments * 6];
        int currentTriangle = 0;

        for (int i = 0; i < verticalSegments; i++)
        {
            for (int j = 0; j < horizontalSegments; j++)
            {
                int currentVertex = i * (horizontalSegments + 1) + j;

                triangles[currentTriangle] = currentVertex;
                triangles[currentTriangle + 1] = currentVertex + horizontalSegments + 1;
                triangles[currentTriangle + 2] = currentVertex + 1;
                triangles[currentTriangle + 3] = currentVertex + 1;
                triangles[currentTriangle + 4] = currentVertex + horizontalSegments + 1;
                triangles[currentTriangle + 5] = currentVertex + horizontalSegments + 2;

                currentTriangle += 6;
            }
        }

        // Asignar datos al mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    private void OnValidate()
    {
        if (Application.isPlaying && meshFilter != null)
        {
            GenerateCurvedScreen();
        }
    }
}