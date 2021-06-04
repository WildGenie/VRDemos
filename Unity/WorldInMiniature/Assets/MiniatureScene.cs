using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HTC.UnityPlugin.Vive;

public class MiniatureScene : MonoBehaviour
{
    public float PreviewScaleFactor = 0.025f;    

    public string TargetSceneName = "MiniWorld";

    public GameObject SourceObjectRoot;
    public GameObject PreviewRoot;

    private GameObject[] _objects;
    private GameObject[] _previewObjects;
    private Vector3[] _initPreviewPositions;    

    private bool isPreviewing = false;
    
    private Vector3 tablePos;

    

    private void Start()
    {
        //vrOrigin = GameObject.Find("VR_Origin");
        //initHmdPos = bab.transform.position;
        //initRhsPos = GameObject.Find("Right").transform.position;
        //PreviewRoot = GameObject.Find("PreviewRoot");
        //SourceObjectRoot = GameObject.Find("SceneObjects");
    }

    private void Update()
    {
        if(isPreviewing)
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
    }

    public void PreviewObjects()
    {
        //var objectRoot = GameObject.Find("SceneObjects");        
        var tmpObjects = new GameObject[SourceObjectRoot.transform.childCount]; 
        for(int i = 0; i < tmpObjects.Length; i++)
        {
            tmpObjects[i] = SourceObjectRoot.transform.GetChild(i).gameObject;
        }

        _objects = new GameObject[tmpObjects.Length];        
        _previewObjects = new GameObject[tmpObjects.Length];
        _initPreviewPositions = new Vector3[tmpObjects.Length];
        var table = GameObject.Find("Table");

        for (int i = 0; i < _objects.Length; i++)
        {
            bool changeMat = false;
            if (tmpObjects[i].name.Equals("Plane"))
            {
                changeMat = true;
            }

            // Cache object
            _objects[i] = tmpObjects[i];
            
            GameObject obj = Instantiate(tmpObjects[i], PreviewRoot.transform);

            if(changeMat)
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                renderer.material.color = Color.blue;
            }
            
            // Scale preview
            obj.transform.localScale = new Vector3(PreviewScaleFactor, PreviewScaleFactor, PreviewScaleFactor);
            Vector3 pos = obj.transform.position;
            float x = pos.x * PreviewScaleFactor;
            float y = pos.y * PreviewScaleFactor;
            float z = pos.z * PreviewScaleFactor;

            tablePos = table.transform.position;
            Vector3 newPos = new Vector3(tablePos.x, tablePos.y, tablePos.z);
            newPos.x += x;
            newPos.y += y;
            newPos.z += z;

            //newPos.y += 0.251f;
            newPos.y = 0f;
            obj.transform.position = newPos;

            _previewObjects[i] = obj;
            _initPreviewPositions[i] = obj.transform.position;
        }

        isPreviewing = true;

    }

    public void TravelToMiniWorld()
    {
        StartCoroutine(AsyncTravelToMiniWorld());
    }

    private IEnumerator AsyncTravelToMiniWorld()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation newSceneLoaded = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while(!newSceneLoaded.isDone)
        {
            yield return null;
        }

        Scene newScene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.MoveGameObjectToScene(SourceObjectRoot, newScene);

        AsyncOperation unloaded = SceneManager.UnloadSceneAsync(currentScene);
        while(!unloaded.isDone)
        {
            yield return null;
        }
    }

    
}
