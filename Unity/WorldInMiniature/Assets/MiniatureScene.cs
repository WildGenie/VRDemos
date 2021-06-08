using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HTC.UnityPlugin.Vive;
using System;

public class MiniatureScene : MonoBehaviour
{
    [Header("Parents")]
    public GameObject SourceObjectRoot;
    public GameObject PreviewRoot;

    [Header("Table Preview")]
    [Range(0.01f, 1f)]
    public float TableScale = 0.25f;

    [Space]
    public GameObject PreviewTable;

    [Header("Hand Preview")]
    [Range(0.01f, 1f)]
    public float HandScale = 0.025f;
    

    [Header("Scene Transition")]
    public string TargetSceneName = "MiniWorld";

    private bool initPreviewObjects = false;

    public enum PrevPos { Init = 0, Table = 1, Hand = 2 };
    private PrevPos currentPrevPos;

    private Vector3[] _lastPos;


    private void Update()
    {
        if (currentPrevPos == PrevPos.Hand)
        {
            UpdatePreviewObjectsInHand();
            //MapPreviewChangesToSourceObjects();            
        }        

    }

    #region Preview

    private void PreviewObjectsOnTable()
    {
        if(!initPreviewObjects) InitializePreviewObjects();

        Vector3 newPos = PreviewTable.transform.position;
        
        // Make sure objects get placed on top of table (y-Direction)
        // ToDo: Generalize this for other kinds of rendering components
        MeshRenderer tableRenderer = PreviewTable.GetComponent<MeshRenderer>();
        newPos.y += (tableRenderer.bounds.size.y * 0.5f) + 0.01f;
        PreviewRoot.transform.position = newPos;

        // Scale preview
        PreviewRoot.transform.localScale = new Vector3(TableScale, TableScale, TableScale);

        // Reset rotation
        PreviewRoot.transform.rotation = Quaternion.identity;
    }

    public void ChangePreviewPos(int newPos)
    {
        currentPrevPos = (PrevPos)newPos;
        switch(currentPrevPos)
        {
            case PrevPos.Table:
                PreviewRoot.transform.localScale = new Vector3(TableScale, TableScale, TableScale);
                PreviewObjectsOnTable();
                break;

            case PrevPos.Hand:
                PreviewRoot.transform.localScale = new Vector3(HandScale, HandScale, HandScale);
                break;
        }
    }

    private void MapDrag(Draggable arg0)
    {
        GameObject grabbedObject = arg0.gameObject;
        int index = -1;
        for(int i = 0; i < PreviewRoot.transform.childCount; i++)
        {
            GameObject child = PreviewRoot.transform.GetChild(i).gameObject;
            if(child == grabbedObject)
            {
                index = 1;
                break;
            }
        }

        if (index == -1) return;

        GameObject objToMove = SourceObjectRoot.transform.GetChild(index).gameObject;
        Vector3 offSet = arg0.posOffset;
        objToMove.transform.position += offSet;        
    }

    
    private void UpdatePreviewObjectsInHand()
    {
        if (!initPreviewObjects) InitializePreviewObjects();

        var pose1 = VivePose.GetPoseEx(HandRole.RightHand);
        
        Vector3 vrOrgPos = GameObject.Find("VR_Origin").transform.position;
        Vector3 offset = pose1.pos;
        offset.y += 0.05f;
        PreviewRoot.transform.position = vrOrgPos + offset;
        
        // Apply controller rotation to scene preview
        PreviewRoot.transform.rotation = pose1.rot;        
    }

    private void InitializePreviewObjects()
    {
        // Get all children of source object
        GameObject[] tmpObjects = new GameObject[SourceObjectRoot.transform.childCount];
        for (int i = 0; i < tmpObjects.Length; i++)
        {
            tmpObjects[i] = SourceObjectRoot.transform.GetChild(i).gameObject;
        }

        // Init array to save preview object positions each frame
        _lastPos = new Vector3[tmpObjects.Length];

        //if (PreviewRoot is null)
        //{
        //    // Create empty root
        //    PreviewRoot = Instantiate(new GameObject("PreviewRoot"), Vector3.zero, Quaternion.identity);
        //}


        // Create a preview clone for every child of source object
        for (int i = 0; i < SourceObjectRoot.transform.childCount; i++)
        {
            bool changeMat = false;
            if (tmpObjects[i].name.Equals("Plane"))
            {
                changeMat = true;
            }

            GameObject obj = Instantiate(tmpObjects[i], PreviewRoot.transform);
            //obj.name = "Preview_" + tmpObjects[i].name;

            if (changeMat)
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                renderer.material.color = Color.blue;
            }

            // Remove VIU teleportable to prevent teleporting onto table
            Teleportable telePort = obj.GetComponent<Teleportable>();
            if (obj != null)
                Destroy(telePort);

            //Rigidbody rigB = obj.AddComponent<Rigidbody>();
            //rigB.constraints = RigidbodyConstraints.FreezeAll;            

            // If object is not a ground plane, make it draggable
            if (!changeMat)
            {
                Draggable drag = obj.AddComponent<Draggable>();
            }

        }

        initPreviewObjects = true;
    }

    private void MapPreviewChangesToSourceObjects()
    {
        for (int i = 0; i < PreviewRoot.transform.childCount; i++)
        {
            Transform child = PreviewRoot.transform.GetChild(i);
            
            // ToDo: Add concrete check if this is a plane if we are doing this at all
            if(child.gameObject.name.Contains("Plane")) continue;

            Vector3 currentPos = child.transform.position;
            if (currentPos != _lastPos[i])
            {
                Vector3 offSet = currentPos - _lastPos[i];
                //offSet *= InvScaleFactor;
                Vector3 newPos = SourceObjectRoot.transform.GetChild(i).position + offSet;
                SourceObjectRoot.transform.GetChild(i).position = newPos; // * InvScaleFactor;
            }
            _lastPos[i] = currentPos;
        }
    }

    #endregion Preview

    #region SceneTransition

    public void TravelToMiniWorld()
    {
        StartCoroutine(AsyncTravelToMiniWorld());
    }

    private IEnumerator AsyncTravelToMiniWorld()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation newSceneLoaded = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!newSceneLoaded.isDone)
        {
            yield return null;
        }

        Scene newScene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.MoveGameObjectToScene(SourceObjectRoot, newScene);

        AsyncOperation unloaded = SceneManager.UnloadSceneAsync(currentScene);
        while (!unloaded.isDone)
        {
            yield return null;
        }
    }

    #endregion SceneTransition

}
