using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class DirectionalLight : Light
    {
        public DirectionalLight()
        {
            direction = new Vector3();
        }

        public Vector3 direction;

        public override Vector3 GetDiraction(Vector3 atPose)
        {
            return direction;
        }

        public override Vector3 GetPosition()
        {
            return (direction * -1000);
        }
    }
}
