using System;
using UnityEngine;

namespace Enemies
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private int resolution;
        [SerializeField] private LayerMask wallsLayerMask;
        [SerializeField] private LayerMask playerLayerMask;

        public float FieldOfView
        {
            set => _fieldOfView = value;
        }
        public float Range
        {
            set => _range = value;
        }
        
        private float _fieldOfView;
        private float _range;
        public bool Scan(bool ignoreMask)
        {
            // Arrays for mesh
            int[] triangles = new int[6 * resolution];
            Vector3[] vertexes = new Vector3[2 * resolution + 2];
            float step = _fieldOfView * Mathf.Deg2Rad / resolution;
            
            // Create field of view
            vertexes[0] = Vector3.zero;
            for (int i = 0; i < resolution; i++) 
            {
                vertexes[i + 1] = Vector3.RotateTowards(
                    Vector3.left * _range, 
                    Vector3.down, 
                    step * (resolution - i),
                    0f
                    );
            }
            vertexes[resolution + 1] = Vector3.left * _range;
            for (int i = 0; i < resolution; i++) 
            {
                vertexes[resolution + i + 2] = Vector3.RotateTowards(
                    Vector3.left * _range, 
                    Vector3.up, 
                    step * i,
                    0f
                );
            }
            
            // Raycast walls
            for (int i = 1; i < vertexes.Length; i++)
            {
                Vector3 position = transform.position;
                
                RaycastHit2D hit = Physics2D.Raycast(
                    position,
                    transform.TransformPoint(vertexes[i]) - position,
                    _range,
                    wallsLayerMask
                );
                
                if (hit.collider)
                    vertexes[i] = transform.InverseTransformPoint(hit.point);
            }
            
            // Raycast Player
            bool playerCaught = false;
            for (int i = 1; i < vertexes.Length; i++)
            {
                Vector3 position = transform.position;
                
                RaycastHit2D hit = Physics2D.Raycast(
                    position,
                    transform.TransformPoint(vertexes[i]) - position,
                    vertexes[i].magnitude,
                    playerLayerMask
                );

                if (hit.collider)
                {
                    if (!ignoreMask)
                        playerCaught = hit.collider.gameObject.CompareTag("Unmasked");
                    else
                        playerCaught = true;
                }
            }
            
            // Set up triangles
            for (int i = 0; i < 6 * resolution; i+=3)
            {
                triangles[i] = 0;
                triangles[i + 1] = (int) (i / 3 + 1);
                triangles[i + 2] = (int) (i / 3 + 2);
            }
            
            // Configure mesh
            Mesh mesh = new Mesh();
            mesh.vertices = vertexes;
            mesh.triangles = triangles;
            GetComponent<MeshFilter>().mesh = mesh;

            return playerCaught;
        }
    }
}