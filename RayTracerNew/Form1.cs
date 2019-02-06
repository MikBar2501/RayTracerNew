using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracerNew
{
    public partial class Form1 : Form
    {
        Raytracer raytrace = new Raytracer();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            raytrace.SetPath(textBox1.Text);
            pictureBox1.Image = raytrace.Raytracing();
        }
    }
}
