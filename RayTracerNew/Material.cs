using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Material
    {
        public MyColor ambient;
        public MyColor diffuse;
        public MyColor specular;
        public MyColor emission;
        public float shininess;

        public Material()
        {
            ambient = MyColor.Black;
            diffuse = MyColor.Black;
            specular = MyColor.Black;
            emission = MyColor.Black;
            shininess = 0;
        }

        public Material(Material material)
        {
            ambient = material.ambient;
            diffuse = material.diffuse;
            specular = material.specular;
            emission = material.emission;
            shininess = material.shininess;
        }
    }
}
