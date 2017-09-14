// The MIT License(MIT)
// Copyright(c) 2017 Borgmon.me

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.iOS;
<<<<<<< refs/remotes/origin/master
namespace ArKitKat
{
    public class VerticalPlaneDetection : MonoBehaviour
    {
        public GameObject verticalPlane;
        private UnityARAnchorManager unityARAnchorManager;
        public float collisionPrefix = 0.9f;
        List<GameObject> verticalPlaneList = new List<GameObject>();

        public Dictionary<string, List<GameObject>> verticalPlanes = new Dictionary<string, List<GameObject>>();
        private bool adjusted;

        // Use this for initialization
        void Start()
        {
            unityARAnchorManager = new UnityARAnchorManager();

        }
        void Update()
        {
            List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();
            if (arpags.Count >= 2)
            {
                arpags = arpags.OrderByDescending(i => i.gameObject.transform.position.y).ToList();

                for (int i = 0; i < arpags.Count; i++)
                {
                    if (i + 1 < arpags.Count)
                    {

                        if (findPlaneUnder(arpags[i].planeAnchor) != null)
                        {
                            if (verticalPlanes.ContainsKey(arpags[i].planeAnchor.identifier))
                            {
                                updateVerticalPlanes(
                                    arpags[i].planeAnchor,
                                    arpags.Find(e => e.planeAnchor.identifier ==
                                    findPlaneUnder(arpags[i].planeAnchor)).planeAnchor);
                            }
                            else
                            {
                                createNewEntry(arpags[i].planeAnchor);
                            }
                            // ArDebug.VirtualAnchor("blue", arpags[i].planeAnchor);

                        }
                    }
                }



                checkDestoryed();
            }

            private string findPlaneUnder(ARPlaneAnchor planeAnchor)
            {
                Vector3 topPos = PlaneTools.GetRealPosition(planeAnchor);
                RaycastHit hit;
                if (Physics.Raycast(topPos, -Vector3.up, out hit, 10f))
                {
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag == "HoriPlane")
                    {
                        return hit.transform.parent.gameObject.name;
                    }
                }
                return null;
            }

            public void updateVerticalPlanes(ARPlaneAnchor topPlane, ARPlaneAnchor btmPlane)
            {
                Vector3 topPos = PlaneTools.GetRealPosition(topPlane);
                Vector3 btmPos = PlaneTools.GetPosition(btmPlane);
                Quaternion topRot = PlaneTools.GetRotation(topPlane);

                float cloneY = topPos.y - (topPos.y - btmPos.y) / 2;
                Vector3 centerPos = new Vector3(topPos.x, cloneY, topPos.z);

                // GameObject planeL = Instantiate(verticalPlane, centerPos, topRot) as GameObject;

                ArDebug.VirtualAnchor("red", centerPos);

                verticalPlanes[topPlane.identifier][0].transform.localScale = new Vector3(topPlane.extent.x * collisionPrefix, topPos.y - btmPos.y, 0.01f);
                verticalPlanes[topPlane.identifier][0].transform.position = centerPos + verticalPlanes[topPlane.identifier][0].transform.forward * (topPlane.extent.z / 2) * collisionPrefix;
                verticalPlanes[topPlane.identifier][0].transform.rotation = topRot;
                adjustLayers(verticalPlanes[topPlane.identifier][0]);

                verticalPlanes[topPlane.identifier][1].transform.localScale = new Vector3(topPlane.extent.x * collisionPrefix, topPos.y - btmPos.y, 0.01f);
                verticalPlanes[topPlane.identifier][1].transform.position = centerPos - verticalPlanes[topPlane.identifier][1].transform.forward * (topPlane.extent.z / 2) * collisionPrefix;
                verticalPlanes[topPlane.identifier][1].transform.rotation = topRot;
                // verticalPlanes[topPlane.identifier][1].transform.Rotate(0, 180, 0);
                adjustLayers(verticalPlanes[topPlane.identifier][1]);

                verticalPlanes[topPlane.identifier][2].transform.rotation = topRot;
                verticalPlanes[topPlane.identifier][2].transform.Rotate(0, 90, 0);
                verticalPlanes[topPlane.identifier][2].transform.localScale = new Vector3(topPlane.extent.z * collisionPrefix, topPos.y - btmPos.y, 0.01f);
                verticalPlanes[topPlane.identifier][2].transform.position = centerPos + verticalPlanes[topPlane.identifier][2].transform.forward * (topPlane.extent.x / 2) * collisionPrefix;
                adjustLayers(verticalPlanes[topPlane.identifier][2]);

                verticalPlanes[topPlane.identifier][3].transform.rotation = topRot;
                verticalPlanes[topPlane.identifier][3].transform.Rotate(0, 90, 0);
                verticalPlanes[topPlane.identifier][3].transform.localScale = new Vector3(topPlane.extent.z * collisionPrefix, topPos.y - btmPos.y, 0.01f);
                verticalPlanes[topPlane.identifier][3].transform.position = centerPos - verticalPlanes[topPlane.identifier][3].transform.forward * (topPlane.extent.x / 2) * collisionPrefix;
                // verticalPlanes[topPlane.identifier][3].transform.Rotate(0, 180, 0);
                adjustLayers(verticalPlanes[topPlane.identifier][3]);

                adjusted = false;
            }
            private void adjustLayers(GameObject plane)
            {
                MeshFilter[] mfList = plane.GetComponentsInChildren<MeshFilter>();

                if (mfList.Length != 0 && !adjusted)
                {
                    int layer = 0;
                    foreach (MeshFilter mf in mfList)
                    {
                        //convert our center position to unity coords
                        mf.gameObject.transform.localPosition = new Vector3(0, 0, -0.01f * layer);
                        layer++;
                        // Debug.Log(mf.gameObject.transform.localPosition.z - 0.01f * layer);
                    }
                }
                adjusted = true;
            }
            private void createNewEntry(ARPlaneAnchor plane)
            {
                List<GameObject> list = new List<GameObject>();
                for (int i = 0; i <= 3; i++)
                {
                    GameObject a = Instantiate(verticalPlane);
                    a.tag = "verticalPlane";
                    list.Add(a);
                }
                verticalPlanes.Add(plane.identifier, list);

            }
            private void updateDict(ARPlaneAnchor plane, GameObject game)
            {
                // if (verticalPlanes.TryGetValue(plane.identifier, out List<GameObject> list))
                // {
                //     list.Add(new GameObject());

                // }
            }
            void checkDestoryed()
            {
                // List<GameObject> allVerPlanes = GameObject.FindGameObjectsWithTag("verticalPlane").ToList();
                List<string> keyList = verticalPlanes.Keys.ToList();
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (GameObject.Find(keyList[i]) == null)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Destroy(verticalPlanes[keyList[i]][j]);
                        }
                        verticalPlanes.Remove(keyList[i]);
                    }
                }
            }
        }
    }
=======
using ArKitKat;

public class VerticalPlaneDetection : MonoBehaviour
{
    public GameObject verticalPlane;
    private UnityARAnchorManager unityARAnchorManager;
    public float collisionPrefix = 0.9f;
    List<GameObject> verticalPlaneList = new List<GameObject>();

    public Dictionary<string, List<GameObject>> verticalPlanes = new Dictionary<string, List<GameObject>>();
    private bool adjusted;

    // Use this for initialization
    void Start()
    {
        unityARAnchorManager = new UnityARAnchorManager();

    }
    void Update()
    {
        List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors();
        if (arpags.Count >= 2)
        {
            arpags = arpags.OrderByDescending(i => i.gameObject.transform.position.y).ToList();

            for (int i = 0; i < arpags.Count; i++)
            {
                if (i + 1 < arpags.Count)
                {

                    if (findPlaneUnder(arpags[i].planeAnchor) != null)
                    {
                        if (verticalPlanes.ContainsKey(arpags[i].planeAnchor.identifier))
                        {
                            updateVerticalPlanes(
                                arpags[i].planeAnchor,
                                arpags.Find(e => e.planeAnchor.identifier ==
                                findPlaneUnder(arpags[i].planeAnchor)).planeAnchor);
                        }
                        else
                        {
                            createNewEntry(arpags[i].planeAnchor);
                        }
                        // ArDebug.VirtualAnchor("blue", arpags[i].planeAnchor);

                    }
                }
            }



            checkDestoryed();
        }
    }

    private string findPlaneUnder(ARPlaneAnchor planeAnchor)
    {
        Vector3 topPos = PlaneTools.GetRealPosition(planeAnchor);
        RaycastHit hit;
        if (Physics.Raycast(topPos, -Vector3.up, out hit, 10f))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "HoriPlane")
            {
                return hit.transform.parent.gameObject.name;
            }
        }
        return null;
    }

    public void updateVerticalPlanes(ARPlaneAnchor topPlane, ARPlaneAnchor btmPlane)
    {
        Vector3 topPos = PlaneTools.GetRealPosition(topPlane);
        Vector3 btmPos = PlaneTools.GetPosition(btmPlane);
        Quaternion topRot = PlaneTools.GetRotation(topPlane);

        float cloneY = topPos.y - (topPos.y - btmPos.y) / 2;
        Vector3 centerPos = new Vector3(topPos.x, cloneY, topPos.z);

        // GameObject planeL = Instantiate(verticalPlane, centerPos, topRot) as GameObject;

        ArDebug.VirtualAnchor("red", centerPos);

        verticalPlanes[topPlane.identifier][0].transform.localScale = new Vector3(topPlane.extent.x * collisionPrefix, topPos.y - btmPos.y, 0.01f);
        verticalPlanes[topPlane.identifier][0].transform.position = centerPos + verticalPlanes[topPlane.identifier][0].transform.forward * (topPlane.extent.z / 2) * collisionPrefix;
        verticalPlanes[topPlane.identifier][0].transform.rotation = topRot;
        adjustLayers(verticalPlanes[topPlane.identifier][0]);

        verticalPlanes[topPlane.identifier][1].transform.localScale = new Vector3(topPlane.extent.x * collisionPrefix, topPos.y - btmPos.y, 0.01f);
        verticalPlanes[topPlane.identifier][1].transform.position = centerPos - verticalPlanes[topPlane.identifier][1].transform.forward * (topPlane.extent.z / 2) * collisionPrefix;
        verticalPlanes[topPlane.identifier][1].transform.rotation = topRot;
        // verticalPlanes[topPlane.identifier][1].transform.Rotate(0, 180, 0);
        adjustLayers(verticalPlanes[topPlane.identifier][1]);

        verticalPlanes[topPlane.identifier][2].transform.rotation = topRot;
        verticalPlanes[topPlane.identifier][2].transform.Rotate(0, 90, 0);
        verticalPlanes[topPlane.identifier][2].transform.localScale = new Vector3(topPlane.extent.z * collisionPrefix, topPos.y - btmPos.y, 0.01f);
        verticalPlanes[topPlane.identifier][2].transform.position = centerPos + verticalPlanes[topPlane.identifier][2].transform.forward * (topPlane.extent.x / 2) * collisionPrefix;
        adjustLayers(verticalPlanes[topPlane.identifier][2]);

        verticalPlanes[topPlane.identifier][3].transform.rotation = topRot;
        verticalPlanes[topPlane.identifier][3].transform.Rotate(0, 90, 0);
        verticalPlanes[topPlane.identifier][3].transform.localScale = new Vector3(topPlane.extent.z * collisionPrefix, topPos.y - btmPos.y, 0.01f);
        verticalPlanes[topPlane.identifier][3].transform.position = centerPos - verticalPlanes[topPlane.identifier][3].transform.forward * (topPlane.extent.x / 2) * collisionPrefix;
        // verticalPlanes[topPlane.identifier][3].transform.Rotate(0, 180, 0);
        adjustLayers(verticalPlanes[topPlane.identifier][3]);

        adjusted = false;
    }
    private void adjustLayers(GameObject plane)
    {
        MeshFilter[] mfList = plane.GetComponentsInChildren<MeshFilter>();

        if (mfList.Length != 0 && !adjusted)
        {
            int layer = 0;
            foreach (MeshFilter mf in mfList)
            {
                //convert our center position to unity coords
                mf.gameObject.transform.localPosition = new Vector3(0, 0, -0.01f * layer);
                layer++;
                // Debug.Log(mf.gameObject.transform.localPosition.z - 0.01f * layer);
            }
        }
        adjusted = true;
    }
    private void createNewEntry(ARPlaneAnchor plane)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i <= 3; i++)
        {
            GameObject a = Instantiate(verticalPlane);
            a.tag = "verticalPlane";
            list.Add(a);
        }
        verticalPlanes.Add(plane.identifier, list);

    }
    private void updateDict(ARPlaneAnchor plane, GameObject game)
    {
        // if (verticalPlanes.TryGetValue(plane.identifier, out List<GameObject> list))
        // {
        //     list.Add(new GameObject());

        // }
    }
    void checkDestoryed()
    {
        // List<GameObject> allVerPlanes = GameObject.FindGameObjectsWithTag("verticalPlane").ToList();
        List<string> keyList = verticalPlanes.Keys.ToList();
        for (int i = 0; i < keyList.Count; i++)
        {
            if (GameObject.Find(keyList[i]) == null)
            {
                for (int j = 0; j < 4; j++)
                {
                    Destroy(verticalPlanes[keyList[i]][j]);
                }
                verticalPlanes.Remove(keyList[i]);
            }
        }
    }

}
>>>>>>> Hotfix
