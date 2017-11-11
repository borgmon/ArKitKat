using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArKitKat
{
    public class BestFitPlaneFloat : MonoBehaviour
    {
        public void BestFitPlane(Vector3[] points, out Vector3 getCenteroid, out Vector3 getNormal)
        {

            int n = points.Length;
            if (n < 3)
            {
                getCenteroid = Vector3.zero;
                getNormal = Vector3.zero;
            }

            Vector3 sum = Vector3.zero;

            foreach (Vector3 p in points)
            {
                sum = sum + p;

            }
            Vector3 centroid = sum;
            centroid = centroid * (float)(1.0 / n);

            // Calculate full 3x3 covariance matrix, excluding symmetries:
            float xx = 0.0f; float xy = 0.0f; float xz = 0.0f;
            float yy = 0.0f; float yz = 0.0f; float zz = 0.0f;

            foreach (Vector3 p in points)
            {
                Vector3 r = p - centroid;
                xx += r.x * r.x;
                xy += r.x * r.y;
                xz += r.x * r.z;
                yy += r.y * r.y;
                yz += r.y * r.z;
                zz += r.z * r.z;
            }
            xx /= (float)n;
            xy /= (float)n;
            xz /= (float)n;
            yy /= (float)n;
            yz /= (float)n;
            zz /= (float)n;

            Vector3 weighted_dir = Vector3.zero;
            float det_x = yy * zz - yz * yz;
            Vector3 axis_dirX = new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy);
            float weightX = det_x * det_x;
            if (Vector3.Dot(weighted_dir, axis_dirX) < 0.0)
            {
                weightX = -weightX;
            }
            weighted_dir += axis_dirX * weightX;

            float det_y = xx * zz - xz * xz;
            Vector3 axis_dirY = new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx);
            float weightY = det_y * det_y;
            if (Vector3.Dot(weighted_dir, axis_dirY) < 0.0)
            {
                weightY = -weightY;
            }
            weighted_dir += axis_dirY * weightY;

            float det_z = xx * yy - xy * xy;
            Vector3 axis_dirZ = new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z);
            float weightZ = det_z * det_z;
            if (Vector3.Dot(weighted_dir, axis_dirZ) < 0.0)
            {
                weightZ = -weightZ;
            }
            weighted_dir += axis_dirZ * weightZ;


            Vector3 normal = Vector3.Normalize(weighted_dir);
            if (!float.IsInfinity(normal.x) && !float.IsNaN(normal.x) &&
                !float.IsInfinity(normal.y) && !float.IsNaN(normal.y) &&
                !float.IsInfinity(normal.z) && !float.IsNaN(normal.z))
            {
                getCenteroid = centroid;
                getNormal = normal;
            }
            else
            {
                getCenteroid = Vector3.zero;
                getNormal = Vector3.zero;
            }
        }
    }
}
