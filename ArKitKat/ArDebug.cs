// The MIT License(MIT)
// Copyright(c) 2017 Borgmon.me

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace ArKitKat
{
    public class ArDebug
    {
        private static Dictionary<string, GameObject> virtualAnchorList = new Dictionary<string, GameObject>();
        public static void VirtualAnchor(string color, ARPlaneAnchor planeAnchor)
        {
            GameObject sphere = createSphere(color, PlaneTools.GetRealPosition(planeAnchor));
            sphere.GetComponent<Renderer>().material.color = getColor(color);
        }
        public static void VirtualAnchor(string color, Vector3 position)
        {
            GameObject sphere = createSphere(color, position);
            sphere.GetComponent<Renderer>().material.color = getColor(color);
        }
        public static void VirtualAnchor(string color, Transform transform)
        {
            GameObject sphere = createSphere(color, transform.position);
            sphere.GetComponent<Renderer>().material.color = getColor(color);
        }
        public static void VirtualAnchor(string color, Matrix4x4 matrix4X4)
        {
            GameObject sphere = createSphere(color, UnityARMatrixOps.GetPosition(matrix4X4));
            sphere.GetComponent<Renderer>().material.color = getColor(color);
        }

        private static GameObject createSphere(string color, Vector3 position)
        {
            if (virtualAnchorList.ContainsKey(color))
            {
                virtualAnchorList[color].transform.position = position;
                virtualAnchorList[color].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                return virtualAnchorList[color];
            }
            else
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = position;
                sphere.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                virtualAnchorList.Add(color, sphere);
                return sphere;
            }

        }

        private static Color getColor(string color)
        {
            switch (color)
            {
                case "red":
                    return new Color(1, 0, 0);
                case "blue":
                    return new Color(0, 0, 1);
                case "yellow":
                    return new Color(1, 0.92f, 0.016f);
                case "green":
                    return new Color(0, 1, 0);
                case "black":
                    return new Color(0, 0, 0);
                default:
                    return new Color(1, 1, 1);

            }
        }
    }
}