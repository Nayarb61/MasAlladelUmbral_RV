using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        // Obtén los MeshFilters de los objetos hijos
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Crea una lista para almacenar las mallas y matrices de transformación
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // Desactiva los objetos originales si es necesario
            meshFilters[i].gameObject.SetActive(false);
        }

        // Crea un nuevo objeto con MeshFilter y MeshRenderer
        Mesh combinedMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = combinedMesh;
        GetComponent<MeshRenderer>().enabled = true;

        // Combina las mallas en una sola
        combinedMesh.CombineMeshes(combine, true);

        // Optimiza la malla resultante
        combinedMesh.Optimize();

        // Recalcula las normales para evitar problemas de iluminación
        combinedMesh.RecalculateNormals();
    }
}

