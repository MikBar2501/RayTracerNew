﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RayTracerNew
{
    class Raytracer
    {
        public Camera camera;
        public Lighting lighting;
        public int maxDepth;
        public string path;
        public static string savePath = "out.png";

        List<Primitive> primitives;

        public static Raytracer main;

        public void SetPath(string path)
        {
            this.path = path;
        }

        public void AddPrimitive(Primitive primitive)
        {
            //primitive.SetAmbient(lighting.ambient);
            primitives.Add(primitive);
        }

        public static float RadianTuDegree(float radian)
        {
            return (float)(radian * 180 / Math.PI);
        }

        public static float DegreeToRadian(float degree)
        {
            return (float)(degree * Math.PI / (float)180);
        }

        public Raytracer()
        {
            if (main == null)
            {
                main = this;
            }
 
        }

        public Image Raytracing()
        {
            camera = new Camera();
            lighting = new Lighting();
            primitives = new List<Primitive>();

            FileReader reader = new FileReader();
            reader.Read(@path);

            float currentPrecGate = 25;
            //Debug.WriteLine(light.color.ToString());

            foreach (Primitive primitive in primitives)
            {
                Debug.WriteLine("trans");
                reader.DrawMatrix(primitive.transform);
                Debug.WriteLine("rev");
                reader.DrawMatrix(primitive.reverseTransform);
                Debug.WriteLine("*");
                reader.DrawMatrix(primitive.reverseTransform * primitive.transform);
            }

            Debug.WriteLine("Read complited");

            //Size = new System.Drawing.Size(camera.width + 5, camera.height + 40);
            //pictureBox1.Size = new System.Drawing.Size(camera.width, camera.height - 1);

            Bitmap bmp = new Bitmap(camera.width, camera.height-1);

            int pixelsCount = camera.height * camera.width;
            bool noLighting = false;

            for (int y = 1; y < camera.height; y++)
            {
                for (int x = 0; x < camera.width; x++)
                {
                    Ray ray = camera.GetRay(x, y);

                    Color pixelColor = Color.Black;
                    //float x0;
                    float closestPointOfContact = 10000;
                    Vector3 pointOfContact;

                    foreach (Primitive primitive in primitives)
                    {
                        Vector3 norm;

                        if (primitive.Intersect(ray, out pointOfContact, out norm))
                        {
                            float curDistance = Vector3.Distance(camera.position, pointOfContact);
                            if (curDistance < closestPointOfContact)
                            {
                                closestPointOfContact = curDistance;

                                //==LIGHT==//
                                //bool visible = true;
                                foreach (Light light in lighting.lights)
                                {
                                    light.visible = true;

                                    Vector3 lightPose = light.GetPosition();

                                    Ray toLightRay = new Ray(pointOfContact, (light.GetDiraction(pointOfContact) * -1).Normalize()); //poszukaj filmikow, przeanalizuj laski
                                    foreach (Primitive lightPrim in primitives)
                                    {
                                        if (lightPrim == primitive)
                                            continue;

                                        Vector3 toLightPointOfContact;
                                        Vector3 toLightNormal;

                                        if (lightPrim.Intersect(toLightRay, out toLightPointOfContact, out toLightNormal))
                                        {
                                            float oldPointOfContactToLight = Vector3.Distance(pointOfContact, lightPose);
                                            float newPointOfContactToLight = Vector3.Distance(toLightPointOfContact, lightPose);
                                            float oldToNew = Vector3.Distance(pointOfContact, toLightPointOfContact);

                                            if (!float.IsNaN(oldToNew)
                                            && oldPointOfContactToLight > newPointOfContactToLight
                                            && oldPointOfContactToLight > oldToNew)
                                            {
                                                light.visible = false;
                                                //pixelColor = Color.Black;
                                                break;
                                            }
                                        }
                                    }
                                }


                                MyColor myPixelColor = new MyColor(0, 0, 0);

                                myPixelColor = lighting.GetColorAtPoint(ray.origin, pointOfContact, primitive.material, norm);


                                if (!noLighting)
                                {
                                    //==Reflections==//
                                    Ray currentRay = ray;
                                    Vector3 currentNormal = norm;
                                    Vector3 currentPoinOfContact = pointOfContact;
                                    Primitive currentPrimitive = primitive;
                                    MyColor currentSpecular = currentPrimitive.material.specular;
                                    //if (!visible)
                                    //    currentSpecular /= 2;
                                    for (int i = 0; i < maxDepth; i++)
                                    {
                                        bool intersectionFound = false;

                                        Vector3 v = currentRay.direction;
                                        Vector3 n = currentNormal;
                                        Vector3 r = v - n * (2 * (Vector3.Dot(n, v)));

                                        currentRay = new Ray(currentPoinOfContact, r.Normalize());
                                        Vector3 pointBehindIntersected = currentRay.origin + currentRay.direction * 1000;
                                        foreach (Primitive primReflect in primitives)
                                        {
                                            if (currentPrimitive == primReflect)
                                                continue;

                                            if (primReflect.Intersect(currentRay, out currentPoinOfContact, out currentNormal))
                                            {
                                                //if (!float.IsNaN(Vector3.Distance(currentPoinOfContact, currentRay.origin))
                                                //    && Vector3.Distance(currentRay.origin, pointBehindIntersected) > Vector3.Distance(currentPoinOfContact, pointBehindIntersected))
                                                //{
                                                float oldPointOfContactToLight = Vector3.Distance(currentRay.origin, pointBehindIntersected);
                                                float newPointOfContactToLight = Vector3.Distance(currentPoinOfContact, pointBehindIntersected);
                                                float oldToNew = Vector3.Distance(currentRay.origin, currentPoinOfContact);

                                                if (!float.IsNaN(oldToNew)
                                                && oldPointOfContactToLight > newPointOfContactToLight
                                                && oldPointOfContactToLight > oldToNew)
                                                {
                                                    MyColor newColor = lighting.GetColorAtPoint(currentRay.origin, currentPoinOfContact, primReflect.material, currentNormal, true);
                                                    myPixelColor = (myPixelColor + (newColor * currentSpecular));
                                                    currentSpecular = primReflect.material.specular * currentSpecular;
                                                    intersectionFound = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!intersectionFound)
                                            break;
                                    }
                                }

                                pixelColor = myPixelColor.ToColor();

                            }
                        }
                    }
                    bmp.SetPixel(x, camera.height - y - 1, pixelColor);
                }
                float perc = ((float)y / camera.height * 100);

                //if (perc > currentPrecGate)
                {
                    //Wait.wait.SetText(perc + "%");
                    //currentPrecGate += 25;
                }
            }

            //pictureBox1.Image = bmp;

            bmp.Save(savePath);
            return bmp;
        }
    }
}
