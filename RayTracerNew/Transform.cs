using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Transform
    {
        public Matrix translate;
        public Matrix rotation;
        public Matrix scale;

        public Matrix transform;

        public List<Matrix> transList;

        public Transform(Transform trans)
        {
            translate = trans.translate;
            rotation = trans.rotation;
            scale = trans.scale;
            transform = new Matrix();
            transList = new List<Matrix>();
            foreach (Matrix tran in trans.transList)
            {
                transList.Add(tran);
            }
        }

        public Transform()
        {
            translate = new Matrix();
            rotation = new Matrix();
            scale = new Matrix();
            transform = new Matrix();
            transList = new List<Matrix>();
        }

        public Matrix GetMatrix()
        {
            //return translate * rotation * scale;
            transform = new Matrix();
            for (int i = transList.Count - 1; i >= 0; i--)
            {
                transform = transList[i] * transform;
            }
            return transform;
        }
    }
}
