//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{

    /// <summary>
    /// Klasse für die Erzeugung eines Oktaeders während der Laufzeit einer Anwendung.
    /// <remarks>
    /// Die Eckpunkte des Oktaeders werden wie in Blinn
    /// und Brill/Bender, "Computergrafik", in den Einheitswürfel platziert.
    /// Sie finden diese Eckpunkte als Lösung der Aufgabe 1 in der Fallstudie
    /// "Platonische und archimedische Körper".
    /// Der planare Graph für die verwendete Topologie finden wir in Abbildung 4.76.
    /// </remarks>
    /// </summary>
    public class Oktaeder : PolyMesh
    {
        /// <summary>
        /// Wir erzeugen für jedes Face des Tetraeders ein SubMesh.
        /// Auch das Material muss anschließend für jedes SubMesh erzeugt
        /// und zugewiesen werden. Das könnte verwendet werden um
        /// für jedes Face ein eigenes Material zu verwenden.
        /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 6;
            const int numberOfSubMeshes = 8;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            vertices[0] = new Vector3( 0.0f,  0.0f, -1.0f);
            vertices[1] = new Vector3( 0.0f,  0.0f,  1.0f);
            vertices[2] = new Vector3(-1.0f,  0.0f,  0.0f);
            vertices[3] = new Vector3( 1.0f,  0.0f,  0.0f);
            vertices[4] = new Vector3( 0.0f, -1.0f,  0.0f);
            vertices[5] = new Vector3( 0.0f,  1.0f,  0.0f);

            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte.
            topology[0] = new int[3] { 2, 0, 5 };
            topology[1] = new int[3] { 0, 3, 5 };
            topology[2] = new int[3] { 3, 1, 5 };
            topology[3] = new int[3] { 1, 2, 5 };
            topology[4] = new int[3] { 0, 2, 4 };
            topology[5] = new int[3] { 3, 0, 4 };
            topology[6] = new int[3] { 1, 3, 4 };
            topology[7] = new int[3] { 2, 1, 4 };

            // Polygonales Netz erzeugen, Geometrie und Topologie zuweisen
            // Es wäre möglich weniger als vier SubMeshes zu erzeugen,
            // solange wir keine Dreiecke in einem Submesh haben, die eine
            // gemeinsame Kante aufweisen!
            Mesh simpleMesh = new Mesh()
            {
                vertices = vertices,
                subMeshCount = numberOfSubMeshes
            };
            // Wir nutzen nicht aus, dass wir pro Submesh ein eigenes
            // Material verwenden.
            for (int i = 0; i < numberOfSubMeshes; i++)
            {
                simpleMesh.SetTriangles(topology[i], i);
                materials[i] = meshMaterial;
            }

            // Unity die Normalenvektoren und die Bounding-Box berechnen lassen.
            simpleMesh.RecalculateNormals();
            simpleMesh.RecalculateBounds();
            simpleMesh.OptimizeIndexBuffers();

            // Zuweisungen für die erzeugten Komponenten
            this.objectFilter.mesh = simpleMesh;
            this.objectRenderer.materials = materials;
        }
    }
}
