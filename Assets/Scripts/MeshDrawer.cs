using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDrawer : MonoBehaviour
{
    private Mesh mesh;
    private Vector3 lastMousePosition;
    [SerializeField] float thickness = 1f, smoothness;

    [SerializeField] Transform debugvisual1, debugvisual2;
    public bool isDrawing;

    public List<Vector3> positions = new List<Vector3>();
    public List<Quaternion> rotations = new List<Quaternion>();

    bool isGameOver;
    private void Awake()
    {
        mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = MousePos();
        vertices[1] = MousePos();
        vertices[2] = MousePos();
        vertices[3] = MousePos();

        uvs[0] = Vector2.zero;
        uvs[1] = Vector2.zero;
        uvs[2] = Vector2.zero;
        uvs[3] = Vector2.zero;


        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;
        lastMousePosition = MousePos();
        isDrawing = true;
    }
    void Start()
    {

    }

    void Update()
    {
        if (isDrawing) { 
            if (Vector3.Distance(MousePos(), lastMousePosition) > smoothness)
            {
                DrawForward();

            }
        }
        else
        {
            if (rotations.Count <= 0)
            {
                if(!isGameOver)
                {
                    GameManager.Instance.winEvent?.Invoke();
                    isGameOver = true;
                }
                return;
            }
            debugvisual1.rotation = Quaternion.Lerp(debugvisual1.rotation, rotations[rotations.Count-1],Time.deltaTime *5);
        }
            

    }

    private void DrawForward()
    {
        Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
        Vector2[] uvs = new Vector2[mesh.uv.Length + 2];
        int[] triangles = new int[mesh.triangles.Length + 6];
        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uvs, 0);
        mesh.triangles.CopyTo(triangles, 0);

        int vIndex = vertices.Length - 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        Vector3 forwardVector = (MousePos() - lastMousePosition).normalized;
        Vector3 normal = Vector3.up;

        Vector3 nextVertex1 = MousePos() + Vector3.Cross(forwardVector, normal) * thickness;
        Vector3 nextVertex2 = MousePos() + Vector3.Cross(forwardVector, normal * -1) * thickness;

        vertices[vIndex2] = nextVertex1;
        vertices[vIndex3] = nextVertex2;

        uvs[vIndex2] = Vector2.zero;
        uvs[vIndex3] = Vector2.zero;

        int tIndex = triangles.Length - 6;

        triangles[tIndex] = vIndex0;
        triangles[tIndex + 1] = vIndex2;
        triangles[tIndex + 2] = vIndex1;

        triangles[tIndex + 3] = vIndex1;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        lastMousePosition = MousePos();
        positions.Add(lastMousePosition);
        rotations.Add(debugvisual1.localRotation);
        debugvisual1.transform.forward = forwardVector;
    }
    public void DrawBackward()
    {

        List<Vector3> vector3s = mesh.vertices.ToList();
        List<Vector2> uvs = mesh.uv.ToList();
        List<int> triangles2 = mesh.triangles.ToList();

        triangles2.RemoveRange(triangles2.Count - 6, 6);
        vector3s.RemoveRange(vector3s.Count - 2, 2);
        uvs.RemoveRange(uvs.Count - 2, 2);


        mesh.triangles = triangles2.ToArray();
        mesh.vertices = vector3s.ToArray();
        mesh.uv = uvs.ToArray();

        
        rotations.RemoveAt(rotations.Count - 1);
    }

    Vector3 MousePos()
    {
        //   Vector3 vectorPos = Input.mousePosition + Vector3.forward * Camera.main.transform.position.y;
        //   return Camera.main.ScreenToWorldPoint(vectorPos);
        return debugvisual1.position;
    }

}
