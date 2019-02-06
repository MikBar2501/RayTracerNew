using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Primitive
    {
        public Material material;
        public Matrix transform;
        public Matrix reverseTransform;

        public virtual bool Intersect(Ray ray, out Vector3 pointOfContact, out Vector3 normal)
        {
            pointOfContact = new Vector3();
            normal = new Vector3();
            return false;
        }
    }
}
