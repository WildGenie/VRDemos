using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HTC.UnityPlugin.Vive;
using System;

public class MiniatureScene : MonoBehaviour
{
    public float PreviewScaleFactor = 0.025f;

    private float _invScaleFac = float.NaN;
    private float InvScaleFactor { 
        get
        {
            if(float.IsNaN(_invScaleFac))
            {
                _invScaleFac = 1 / PreviewScaleFactor;
            }
            return _invScaleFac;
        }
    }

    public string TargetSceneName = "MiniWorld";

    public GameObject SourceObjectRoot;
    public GameObject PreviewRoot;

    //private GameObject[] _objects;
    //private GameObject[] _previewObjects;
    //private Vector3[] _initPreviewPositions;    

    private bool isPreviewing = false;

    private Vector3 tablePos;

    private Vector3[] _lastPos;

    private void Update()
    {
        if (isPreviewing)
        {
            //UpdatePreviewObjects();
            MapPreviewChangesToSourceObjects();

            
        }

        if(SourceObjectRoot.transform.childCount == PreviewRoot.transform.childCount)
        {
            
        }
    }



    public void PreviewObjects()
    {
        GameObject[] tmpObjects = new GameObject[SourceObjectRoot.transform.childCount];
        for (int i = 0; i < tmpObjects.Length; i++)
        {
            tmpObjects[i] = SourceObjectRoot.transform.GetChild(i).gameObject;
        }

        _lastPos = new Vector3[tmpObjects.Length];

        GameObject table = GameObject.Find("Table");

        
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

            float tableScaleIncrease = 3f;
            // Scale preview
            obj.transform.localScale = new Vector3(
                PreviewScaleFactor * tableScaleIncrease,
                PreviewScaleFactor * tableScaleIncrease,
                PreviewScaleFactor * tableScaleIncrease);

            Vector3 pos = obj.transform.position;
            float x = pos.x * (PreviewScaleFactor * tableScaleIncrease);
            float y = pos.y * (PreviewScaleFactor * tableScaleIncrease);
            float z = pos.z * (PreviewScaleFactor * tableScaleIncrease);



            // Move preview elements to table
            tablePos = table.transform.position;
            Vector3 newPos = new Vector3(tablePos.x, tablePos.y, tablePos.z);
            newPos.x += x;
            newPos.y += y;
            newPos.z += z;

            newPos.y = 0.501f;  // 0f;
            obj.transform.position = newPos;

            var telePort = obj.GetComponent<Teleportable>();
            if (obj != null)
                Destroy(telePort);

            //Rigidbody rigB = obj.AddComponent<Rigidbody>();
            //rigB.constraints = RigidbodyConstraints.FreezeAll;            

            if(!changeMat)
            {
                Draggable drag = obj.AddComponent<Draggable>();
                
                //drag.afterGrabbed.AddListener(MapDrag);
                

            }
                
        }

        // Activate rendering of preview elements
        isPreviewing = true;


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
        
        //pos.x * (PreviewScaleFactor * tableScaleIncrease);

    }

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

    private void UpdatePreviewObjects()
    {
        var pose1 = VivePose.GetPoseEx(HandRole.RightHand);
        var pose2 = VivePose.GetPoseEx(TrackerRole.Tracker1);
        var hmdPose = VivePose.GetPoseEx(DeviceRole.Hmd);

        GameObject o = PreviewRoot;

        // Calculate offset based on current HMD position
        Vector3 offsetVector = hmdPose.TransformPoint(hmdPose.pos);

        offsetVector.z += 2f;
        offsetVector.y = 1.5f;

        Vector3 vrOrgPos = GameObject.Find("VR_Origin").transform.position;
        Vector3 offset = pose1.pos;
        offset.y += 0.125f;
        o.transform.position = vrOrgPos + offset;

        // Apply controller rotation to scene preview
        o.transform.rotation = pose1.rot;
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

}
