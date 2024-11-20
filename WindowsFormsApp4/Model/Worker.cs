using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.Model
{
    [Serializable()]
    public class Worker
    {
        public List<Rectangle> rectA = new List<Rectangle>();
        public List<Color> colorA = new List<Color>();

        public List<Rectangle> rectE = new List<Rectangle>();
        public List<Color> colorE = new List<Color>();

        public List<Point> ptUpA = new List<Point>();
        public List<Point> ptDawnA = new List<Point>();
        public List<Color> colorL = new List<Color>();

        public List<Point> ptUpP = new List<Point>();
        public List<Point> ptDawnP = new List<Point>();
        public List<Color> colorP = new List<Color>();
    }
}
