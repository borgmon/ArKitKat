using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArKitKat
{

    public class BestFitPlaneDouble : MonoBehaviour
    {
        public void BestFitPlane(Vector3[] points, out Vector3 getCenteroid, out Vector3 getNormal)
        {
            List<Vector3d> points3d = new List<Vector3d>();
            foreach (Vector3 p in points)
            {
                points3d.Add(new Vector3d(p));
            }

            int n = points.Length;
            if (n < 3)
            {
                getCenteroid = Vector3.zero;
                getNormal = Vector3.zero;
            }

            Vector3d sum = new Vector3d(Vector3.zero);

            foreach (Vector3d p in points3d)
            {
                sum = sum + p;

            }
            Vector3d centroid = sum;
            centroid = centroid * (1.0 / n);

            // Calculate full 3x3 covariance matrix, excluding symmetries:
            double xx = 0.0; double xy = 0.0; double xz = 0.0;
            double yy = 0.0; double yz = 0.0; double zz = 0.0;

            foreach (Vector3d p in points3d)
            {
                Vector3d r = p - centroid;
                xx += r.x * r.x;
                xy += r.x * r.y;
                xz += r.x * r.z;
                yy += r.y * r.y;
                yz += r.y * r.z;
                zz += r.z * r.z;
            }
            xx /= (double)n;
            xy /= (double)n;
            xz /= (double)n;
            yy /= (double)n;
            yz /= (double)n;
            zz /= (double)n;

            Vector3d weighted_dir = new Vector3d(Vector3.zero);
            double det_x = yy * zz - yz * yz;
            Vector3d axis_dirX = new Vector3d(det_x, xz * yz - xy * zz, xy * yz - xz * yy);
            double weightX = det_x * det_x;
            if (Vector3.Dot((Vector3)weighted_dir, (Vector3)axis_dirX) < 0.0)
            {
                weightX = -weightX;
            }
            weighted_dir += axis_dirX * weightX;

            double det_y = xx * zz - xz * xz;
            Vector3d axis_dirY = new Vector3d(xz * yz - xy * zz, det_y, xy * xz - yz * xx);
            double weightY = det_y * det_y;
            if (Vector3.Dot((Vector3)weighted_dir, (Vector3)axis_dirY) < 0.0)
            {
                weightY = -weightY;
            }
            weighted_dir += axis_dirY * weightY;

            double det_z = xx * yy - xy * xy;
            Vector3d axis_dirZ = new Vector3d(xy * yz - xz * yy, xy * xz - yz * xx, det_z);
            double weightZ = det_z * det_z;
            if (Vector3.Dot((Vector3)weighted_dir, (Vector3)axis_dirZ) < 0.0)
            {
                weightZ = -weightZ;
            }
            weighted_dir += axis_dirZ * weightZ;


            Vector3d normal = Vector3d.Normalize(weighted_dir);
            if (!double.IsInfinity(normal.x) && !double.IsNaN(normal.x) &&
                !double.IsInfinity(normal.y) && !double.IsNaN(normal.y) &&
                !double.IsInfinity(normal.z) && !double.IsNaN(normal.z))
            {
                getCenteroid = (Vector3)centroid;
                getNormal = (Vector3)normal;
            }
            else
            {
                getCenteroid = Vector3.zero;
                getNormal = Vector3.zero;
            }
        }
    }
}
