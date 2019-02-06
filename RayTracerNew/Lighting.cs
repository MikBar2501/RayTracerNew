using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Lighting
    {
        public List<Light> lights;

        public Lighting()
        {
            lights = new List<Light>();
        }

        public MyColor GetColorAtPoint(Vector3 cameraPose, Vector3 point, Material material, Vector3 normal, bool forceVisible = false)
        {
            MyColor ambient = material.ambient;
            MyColor emission = material.emission;

            MyColor outColor = ambient + emission;
            foreach (Light light in lights)
            {
                int visible = 0;
                if (light.visible || forceVisible)
                    visible = 1;

                Vector3 lightDirection = light.GetDiraction(point);
                Vector3 toCamera = (cameraPose - point).Normalize();
                Vector3 halfVector = (lightDirection + toCamera).Normalize();
                MyColor lightColor = light.GetLightColor(point);

                MyColor diffuse = material.diffuse * (Max(0, Vector3.Dot(lightDirection, normal)));
                MyColor specular = material.specular * (float)Math.Pow(Max(0, Vector3.Dot(halfVector, normal)), material.shininess);
                if (forceVisible)
                    specular /= 2;

                outColor += lightColor* visible *(diffuse + specular);
            }
            return outColor;
        }

        float Max(float v1, float v2)
        {
            if (v1 > v2)
                return v1;
            return v2;
        }
    }
}
