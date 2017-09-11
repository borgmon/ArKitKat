// The MIT License(MIT)
// Copyright(c) 2017 Borgmon.me

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace ArKitKat
{
    public class PlaneTools : MonoBehaviour
    {
        public static Vector3 GetRealPosition(ARPlaneAnchor planeAnchor)
        {
            GameObject anchor = new GameObject("Anchor");
            GameObject parent = new GameObject("AnchorParent");
            anchor.transform.SetParent(parent.transform);

            parent.transform.position = UnityARMatrixOps.GetPosition(planeAnchor.transform);
            parent.transform.rotation = UnityARMatrixOps.GetRotation(planeAnchor.transform);
            anchor.transform.localPosition = new Vector3(planeAnchor.center.x, planeAnchor.center.y, -planeAnchor.center.z);
            Vector3 result = anchor.transform.position;
            Destroy(anchor);
            Destroy(parent);
            return result;
            // Vector3 fix = planeAnchor.center;
            // fix = new Vector3(fix.x, fix.y, -fix.z);
            // return UnityARMatrixOps.GetPosition(planeAnchor.transform) + fix;
        }
        public static Vector3 GetPosition(ARPlaneAnchor planeAnchor)
        {
            return UnityARMatrixOps.GetPosition(planeAnchor.transform);
        }
        public static Quaternion GetRotation(ARPlaneAnchor planeAnchor)
        {
            return UnityARMatrixOps.GetRotation(planeAnchor.transform);
        }
    }
}