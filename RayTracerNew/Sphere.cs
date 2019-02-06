using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Sphere : Primitive
    {
        public Vector3 center;
        public float radius;

        public Sphere(Vector3 c, float r)
        {
            center = c;
            radius = r;
        }

        public override bool Intersect(Ray ray, out Vector3 pointOfContact, out Vector3 normal)
        {
            Ray transformedRay = reverseTransform * ray;

            //x = 0;
            Vector3 distance = transformedRay.origin - center;
            float a = Vector3.Dot(transformedRay.direction, transformedRay.direction);
            float b = Vector3.Dot(transformedRay.direction * 2, distance);
            float c = Vector3.Dot(distance, distance) - (radius * radius);
            float delta = b * b - 4 * a * c;
            if (delta <= 0)
            {
                pointOfContact = new Vector3();
                normal = new Vector3();
                return false;
            }
            else
            {
                float deltaSqrt = (float)Math.Sqrt(delta);
                float x1 = (-b - deltaSqrt) / (2 * a);
                float x2 = (-b + deltaSqrt) / (2 * a);

                float x = x1;
                if (x1 > 0 && x2 > 0)
                    x = x1 > x2 ? x2 : x1;

                if ((x1 < 0 && x2 > 0) || (x1 > 0 && x2 < 0))
                    x = x1 < x2 ? x2 : x1;


                pointOfContact = transformedRay.origin + transformedRay.direction * x;
                normal = (pointOfContact - center).Normalize();

                pointOfContact = transform * pointOfContact;
                normal = transform.Inverse().Transpose() * normal;
                normal = normal.Normalize();
                //pointOfContact = transform.Reverse() * pointOfContact;
                //if (Vector3.Dot(pointOfContact - center, pointOfContact - center) - radius * radius != 0)
                //    return false;
                return true;
            }
        }
    }
}
