using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class PointLight : Light
    {
        public PointLight()
        {
            if (constant == 0)
                constant = 1;

            position = new Vector3();
        }

        public Vector3 position;
        public static float constant;
        public static float linear;
        public static float quadratic;

        public override Vector3 GetPosition()
        {
            return position;
        }

        public override Vector3 GetDiraction(Vector3 atPose)
        {
            return (position - atPose).Normalize();
        }

        public override MyColor GetLightColor(Vector3 point)
        {
            float distance = (position - point).Magnitude();
            return color; //* (1 / (constant + linear * distance + distance * distance * quadratic));
        }
    }
}
