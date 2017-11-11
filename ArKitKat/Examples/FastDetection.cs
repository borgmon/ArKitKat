using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArKitKat;
using UnityEngine;
using UnityEngine.XR.iOS;

public class FastDetection : MonoBehaviour
{

    public GameObject planePrefab;
    BestFitPlaneDouble bestFit;
    GameObject planeGo;
    private Vector3[] m_PointCloudData;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
        planeGo = Instantiate(planePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PointCloudData != null)
        {
            drawPlane(m_PointCloudData);
        }
    }
    public void ARFrameUpdated(UnityARCamera camera)
    {
        m_PointCloudData = camera.pointCloudData;
    }
    void drawPlane(Vector3[] points)
    {
        Vector3 planePos, planeNor;
        bestFit.BestFitPlane(points, out planePos, out planeNor);
        planeGo.transform.position = planePos;
        planeGo.transform.rotation = Quaternion.FromToRotation(transform.up, planeNor);
    }

}
