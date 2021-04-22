//========= 2020 - Copyright Manfred Brill. All rights reserved. ===========
using UnityEngine;

/// <summary>
/// Namespace für allgemeine Unity-Assets
/// </summary>
namespace VRKL.MBU
{

    /// <summary>
    /// Klasse für die Erzeugung eines Dodekaeder während der Laufzeit einer Anwendung.
    /// Die 12 Fünfecke werden als TriangleFan realisiert, also pro Fünfeck 
    /// drei Dreiecke.
    /// <remarks>
    /// Die vier Eckpunkte des Tetraeders werden wie in Blinn
    /// und Brill/Bender, "Computergrafik", iauf der Einheitskugel platziert.
    /// </remarks>
    /// </summary>
    public class Dodekaeder : PolyMesh
    {
        /// <summary>
        /// Wir erzeugen für jedes Face des Dodekaeders ein SubMesh.
        /// Auch das Material muss anschließend für jedes SubMesh erzeugt
        /// und zugewiesen werden. Das könnte verwendet werden um
        /// für jedes Face ein eigenes Material zu verwenden.
        /// </summary>
        protected override void Create()
        {
            const int numberOfVertices = 20;
            const int numberOfSubMeshes = 36;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[][] topology = new int[numberOfSubMeshes][];
            Material[] materials = new Material[numberOfSubMeshes];

            // Hilfsvariable für die Eckpunkte
            float A = 0.618034f;
            float B = A + 1.0f;
            // Die 20 Eckpunkte - als Zentrum der Facetten des
            // dazu dualen Ikosaeders
            vertices[0] = new Vector3(1.0f, 1.0f, 1.0f);
            vertices[1] = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[2] = new Vector3(1.0f, -1.0f, 1.0f);
            vertices[3] = new Vector3(1.0f, -1.0f, -1.0f);
            vertices[4] = new Vector3(-1.0f, 1.0f, 1.0f);
            vertices[5] = new Vector3(-1.0f, 1.0f, -1.0f);
            vertices[6] = new Vector3(-1.0f, -1.0f, 1.0f);
            vertices[7] = new Vector3(-1.0f, -1.0f, -1.0f);
            vertices[8] = new Vector3(A, B, 0.0f);
            vertices[9] = new Vector3(-A, B, 0.0f);
            vertices[10] = new Vector3(A, -B, 0.0f);
            vertices[11] = new Vector3(-A, -B, 0.0f);
            vertices[12] = new Vector3(B, 0.0f, A);
            vertices[13] = new Vector3(B, 0.0f, -A);
            vertices[14] = new Vector3(-B, 0.0f, A);
            vertices[15] = new Vector3(-B, 0.0f, -A);
            vertices[16] = new Vector3(0.0f, A, B);
            vertices[17] = new Vector3(0.0f, -A, B);
            vertices[18] = new Vector3(0.0f, A, -B);
            vertices[19] = new Vector3(0.0f, -A, -B);

            // Die Einträge in der Topologie beziehen sich auf 
            // die Indizes der Eckpunkte. Wir rendern jedes
            // Fünfeck mit Hilfe eines Triangle Fans. D
            // Dabei ist der Drehpunkt des Fans durch den ersten
            // Index gegeben.
            topology[0] = new int[3] { 1, 8, 0};
            topology[1] = new int[3] { 1, 0, 12 };
            topology[2] = new int[3] { 1, 12, 13 };

            topology[3] = new int[3] { 4, 9, 5};
            topology[4] = new int[3] { 4, 5, 15};
            topology[5] = new int[3] { 4, 15, 14 };

            topology[6] = new int[3] { 2, 10, 3};
            topology[7] = new int[3] { 2, 3, 13};
            topology[8] = new int[3] { 2, 13, 12 };

            topology[9] = new int[3] { 7, 11, 6};
            topology[10] = new int[3] { 7, 6, 14 };
            topology[11] = new int[3] { 7, 14, 15 };

            topology[12] = new int[3] { 2, 12, 0 };
            topology[13] = new int[3] { 2, 0, 16 };
            topology[14] = new int[3] { 2, 16, 17 };

            topology[15] = new int[3] { 1, 13, 3 };
            topology[16] = new int[3] { 1,  3, 19 };
            topology[17] = new int[3] { 1, 19, 18 };

            topology[18] = new int[3] { 4, 14, 6};
            topology[19] = new int[3] { 4, 6, 17 };
            topology[20] = new int[3] { 4, 17, 16 };

            topology[21] = new int[3] { 7, 15, 5 };
            topology[22] = new int[3] { 7, 5, 18 };
            topology[23] = new int[3] { 7, 18, 19 };

            topology[24] = new int[3] { 4, 16, 0 };
            topology[25] = new int[3] { 4, 0, 8 };
            topology[26] = new int[3] { 4, 8, 9 };

            topology[27] = new int[3] { 2, 17, 6 };
            topology[28] = new int[3] { 2,  6, 11 };
            topology[29] = new int[3] { 2, 11, 10 };

            topology[30] = new int[3] { 1, 18, 5};
            topology[31] = new int[3] { 1,  5, 9};
            topology[32] = new int[3] { 1,  9, 8 };

            topology[33] = new int[3] { 7, 19, 3 };
            topology[34] = new int[3] { 7, 3, 10 };
            topology[35] = new int[3] { 7, 10, 11 };

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
