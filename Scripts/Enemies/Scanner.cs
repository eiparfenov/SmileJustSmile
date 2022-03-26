using System;
using UnityEngine;

namespace Enemies
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private int resolution;
        [SerializeField] private float fieldOfView;
        [SerializeField] private float range;
        [SerializeField] private LayerMask wallsLayerMask;
        private void Update()
        {
            int[] triangles = new int[6 * resolution];
            Vector3[] vertexes = new Vector3[2 * resolution + 2];
            float step = fieldOfView * Mathf.Deg2Rad / resolution;
            
            vertexes[0] = Vector3.zero;
            for (int i = 0; i < resolution; i++) 
            {
                vertexes[i + 1] = Vector3.RotateTowards(
                    Vector3.left * range, 
                    Vector3.down, 
                    step * (resolution - i),
                    0f
                    );
            }
            vertexes[resolution + 1] = Vector3.left * range;
            for (int i = 0; i < resolution; i++) 
            {
                vertexes[resolution + i + 2] = Vector3.RotateTowards(
                    Vector3.left * range, 
                    Vector3.up, 
                    step * i,
                    0f
                );
            }

            for (int i = 1; i < vertexes.Length; i++)
            {
                Vector3 position = transform.position;
                
                RaycastHit2D hit = Physics2D.Raycast(
                    position,
                    transform.TransformPoint(vertexes[i]) - position,
                    range,
                    wallsLayerMask
                );
                
                if (hit.collider)
                    vertexes[i] = transform.InverseTransformPoint(hit.point);
            }

            for (int i = 0; i < 6 * resolution; i+=3)
            {
                triangles[i] = 0;
                triangles[i + 1] = (int) (i / 3 + 1);
                triangles[i + 2] = (int) (i / 3 + 2);
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertexes;
            mesh.triangles = triangles;
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}