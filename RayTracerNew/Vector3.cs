using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerNew
{
    class Vector3
    {
        public float x, y, z;
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Normalize()
        {
            float magnitude = Magnitude();
            return this / magnitude;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vector3 operator *(Vector3 vec, float v)
        {
            return new Vector3(vec.x * v, vec.y * v, vec.z * v);
        }

        public static Vector3 operator /(Vector3 vec, float v)
        {
            return new Vector3(vec.x / v, vec.y / v, vec.z / v);
        }

        public float Lenght()
        {
            return x + y + z;
        }

        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector3 Projection(Vector3 vecTo, Vector3 vecOn)
        {
            return vecOn * ((Vector3.Dot(vecOn, vecTo)) / (Vector3.Dot(vecOn, vecOn)));
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.y * v2.z - v1.z * v2.y,
                v1.z * v2.x - v1.x * v2.z,
                v1.x * v2.y - v1.y * v2.x);
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            return (vec1 - vec2).Magnitude();
        }

        public MyColor ToMyColor()
        {
            return new MyColor(
                Math.Min((int)(x * 255), 255),
                Math.Min((int)(y * 255), 255),
                Math.Min((int)(z * 255), 255));
        }

        public Matrix ToMatrix(int w)
        {
            Matrix mat = new Matrix(4, 1);
            mat.grid[0, 0] = x;
            mat.grid[1, 0] = y;
            mat.grid[2, 0] = z;
            mat.grid[3, 0] = w;

            return mat;
        }
    }
}
