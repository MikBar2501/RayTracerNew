using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracerNew
{
    class MyColor
    {
        public int r;
        public int g;
        public int b;

        public static MyColor Black = new MyColor(0, 0, 0);

        public MyColor(Color color)
        {
            r = color.R;
            g = color.G;
            b = color.B;
        }

        public MyColor(int R, int G, int B)
        {
            r = R;
            g = G;
            b = B;
        }

        public Vector3 ToFloat()
        {
            return new Vector3((float)r / 255, (float)g / 255, (float)b / 255);
        }

        public Color ToColor()
        {
            int r = Math.Min(255, this.r);
            int g = Math.Min(255, this.g);
            int b = Math.Min(255, this.b);
            return Color.FromArgb(r, g, b);
        }

        public string ToString()
        {
            return "R - " + r + " : G - " + g + " : B - " + b;
        }

        public static MyColor operator *(MyColor c1, MyColor c2)
        {
            Vector3 v1 = c1.ToFloat();
            Vector3 v2 = c2.ToFloat();

            Vector3 v = v1 * v2;

            return v.ToMyColor();
        }



        public static MyColor operator *(MyColor c1, float value)
        {
            Vector3 v1 = c1.ToFloat();
            Vector3 v = v1 * value;

            return v.ToMyColor();
        }

        public static MyColor operator /(MyColor c1, float value)
        {
            Vector3 v1 = c1.ToFloat();
            Vector3 v = v1 / value;

            return v.ToMyColor();
        }

        public static MyColor operator +(MyColor c1, MyColor c2)
        {
            return new MyColor(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b);
        }
    }
}
