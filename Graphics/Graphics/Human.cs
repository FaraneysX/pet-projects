using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace GraphicsPractice
{
    internal class Human
    {
        public Human(int width,Color new_color_pants)
        {
            Width_body = width;
            Height_body = 350;
            X = Center_X-(width/2);
            Y = 141;
            Jacket_color = Color.Black;
            Pants_color = new_color_pants;
            Body_color = Color.Moccasin;
            Zip_color = Color.Gray;
            body = new Rectangle(X, 144, Width_body, Height_body);
            Pants = new Point[]
            {
            new Point(X, 329),
            new Point(330, 617),
            new Point(385, 617),
            new Point(385, 497),
            new Point(399, 497),
            new Point(399, 617),
            new Point(450, 617),
            new Point(X + Width_body, 329),
            };
            Bottom_jacket = new Point[]
            {
                new Point(X,322),
                new Point(X,427),
                new Point(X+Width_body,427),
                new Point(X+Width_body,322)
            };
            Zip = new Rectangle(386, 147, 10, 280);
            head = new Rectangle(349, 45, 86, 100);
            left_leg = new Rectangle(330, 617, 55, 55);
            right_leg = new Rectangle(399, 617, 55, 55);
            left_hand = new Point[] { new Point(X - 109, 348), new Point(X - 82, 381), new Point(X, 281), new Point(X + 28, 190) };
            right_hand = new Point[] { new Point(X + width + 109, 348), new Point(X + width + 82, 381), new Point(X + width, 281), new Point(X+width - 28, 190) };
            jacket.AddEllipse(body);
            jacket.AddPolygon(left_hand);
            jacket.AddPolygon(right_hand);
            jacket.AddPolygon(Bottom_jacket);
            jacket.AddRectangle(Zip);
        }
        public Color Jacket_color;
        public Color Pants_color;
        public Color Body_color;
        public Color Zip_color;
        private int Width_body;
        private int Height_body;
        private int Center_X = 390;
        public int X;
        public int Y;
        public Rectangle body;
        public Rectangle head;
        public Point[] Pants;
        public Point[] Bottom_jacket;
        public Rectangle Zip;
        public Rectangle left_leg;
        public Rectangle right_leg;
        public Point[] left_hand;
        public Point[] right_hand;


        public GraphicsPath jacket=new GraphicsPath();
        public List<GraphicsPath> list_rivet=new List<GraphicsPath>();

        public void AddRivet(int x,int y)
        {
            if (jacket.IsVisible(new Point(x, y)))
            { 
                GraphicsPath new_rivet = new GraphicsPath();
                new_rivet.AddEllipse(x, y, 10, 10);
                list_rivet.Add(new_rivet); 
            }
        }
        public void DeleteRivet(Point rivet)
        {
            list_rivet.RemoveAll(r => r.IsVisible(rivet));
        }
    }
}