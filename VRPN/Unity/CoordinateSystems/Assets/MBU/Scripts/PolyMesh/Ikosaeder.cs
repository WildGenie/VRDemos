//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{

    /// <summary>
    /// Klasse für die Erzeugung eines Ikosaeders während der Laufzeit einer Anwendung.
    /// <remarks>
    /// Die Eckpunkte des Ikosaeders werden wie in Blinn
    /// und Brill/Bender, "Computergrafik", in den Einheitswürfel platziert.
    /// </remarks>
    /// </summary>
    public class Ikosaeder : PolyMesh
    {
        /// <summary>
        /// Wir erzeugen für jedes Face des Tetraeders ein SubMesh.
        /// Auch das Material muss anschließend für jedes SubMesh erzeugt
        /// und zugewiesen werden. Das könnte verwendet werden um
        /// für jedes Face ein eigenes Material zu verwenden.
        /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 12;
            const int numberOfSubMeshes = 20;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            // Hilfsgrößen für die Eckpunktkoordinaten
            // Tau, der Kehrwert des goldenen Schnitts
            float X = 0.525731112119133606f;
            float Z = 0.850650808352039932f;

            vertices[0] = new Vector3( -X, 0.0f, Z );
            vertices[1] = new Vector3(  X, 0.0f, Z );
            vertices[2] = new Vector3( -X, 0.0f, -Z );
            vertices[3] = new Vector3(  X, 0.0f, -Z ) ;
            vertices[4] = new Vector3( 0.0f, Z,  X ) ;
            vertices[5] = new Vector3( 0.0f, Z, -X);
            vertices[6] = new Vector3( 0.0f, -Z, X);
            vertices[7] = new Vector3( 0.0f, -Z, -X);
            vertices[8] = new Vector3( Z, X, 0.0f );
            vertices[9] = new Vector3(-Z, X, 0.0f );
            vertices[10] = new Vector3( Z, -X, 0.0f);
            vertices[11] = new Vector3( -Z, -X, 0.0f);

            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte.
            topology[0] = new int[3] { 1, 4, 0 };
            topology[1] = new int[3] { 4, 9 , 0 };
            topology[2] = new int[3] { 4, 5, 9 };
            topology[3] = new int[3] { 8, 5, 4 };
            topology[4] = new int[3] { 1, 8, 4};
            topology[5] = new int[3] { 1, 10, 8 };
            topology[6] = new int[3] { 10, 3, 8 };
            topology[7] = new int[3] { 8, 3, 5 };
            topology[8] = new int[3] { 3, 2, 5 };
            topology[9] = new int[3] { 3, 7, 2 };
            topology[10] = new int[3] { 3, 10, 7 };
            topology[11] = new int[3] { 10, 6, 7 };
            topology[12] = new int[3] { 6, 11, 7};
            topology[13] = new int[3] { 6, 0, 11 };
            topology[14] = new int[3] { 6, 1, 0 };
            topology[15] = new int[3] { 10, 1, 6 };
            topology[16] = new int[3] { 11, 0, 9};
            topology[17] = new int[3] { 2, 11, 9 };
            topology[18] = new int[3] { 5, 2, 9 };
            topology[19] = new int[3] { 11, 2, 7 };

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
