using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Outline script creates a duplicate mesh with the outline shader attached
public class Outline : MonoBehaviour
{
    [SerializeField] float lineThickness = 0.1f;
    [SerializeField] Color lineColor = Color.black;
    Material outlineMaterial;

    GameObject outlineInstance;

    private void Awake()
    {
        outlineInstance = new GameObject(gameObject.name + " (outline)");
        
        outlineInstance.transform.parent = gameObject.transform;
        outlineMaterial = new Material(Shader.Find("Shader Graphs/Outline"));

        outlineInstance.transform.localPosition = Vector3.zero;
        outlineInstance.transform.localRotation = Quaternion.identity;
        outlineInstance.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    private void Start()
    {
        MeshFilter ogMeshFilter = gameObject.GetComponent<MeshFilter>();
        MeshFilter meshFilter = outlineInstance.AddComponent<MeshFilter>();
        meshFilter.mesh = ogMeshFilter.mesh;
        if (ogMeshFilter.mesh.subMeshCount == 1)
        {
            
        } else
        {
            Mesh[] meshes = new Mesh[ogMeshFilter.mesh.subMeshCount];
            for(int i = 0; i < ogMeshFilter.mesh.subMeshCount; i++)
            {
                
            }
        }
        MeshRenderer meshRenderer = outlineInstance.AddComponent<MeshRenderer>();
        outlineMaterial.SetFloat("_LineThickness", lineThickness);
        outlineMaterial.SetColor("_LineColor", lineColor);
        meshRenderer.material = outlineMaterial;


    }
}
