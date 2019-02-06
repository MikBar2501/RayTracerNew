using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Light
    {
        public MyColor color;
        public bool visible;

        public virtual Vector3 GetDiraction(Vector3 atPose)
        {
            return new Vector3();
        }

        public virtual MyColor GetLightColor(Vector3 point)
        {
            return color;
        }

        public virtual Vector3 GetPosition()
        {
            return new Vector3();
        }
    }
}
