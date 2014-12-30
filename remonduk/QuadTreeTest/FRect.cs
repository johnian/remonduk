// // // // // // // // // // // // //
// QuadTree and supporting code
// by Kyle Schouviller
// http://www.kyleschouviller.com
//
// December 2006: Original version
// May 06, 2007:  Updated for XNA Framework 1.0
//                and public release.
//
// You may use and modify this code however you
// wish, under the following condition:
// *) I must be credited
// A little line in the credits is all I ask -
// to show your appreciation.
// 
// If you have any questions, please use the
// contact form on my website.
//
// Now get back to making great games!
// // // // // // // // // // // // //

#region Using declarations

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

#endregion

namespace remonduk.QuadTreeTest
{
    /// <summary>
    /// A floating-point rectangle
    /// </summary>
    public struct FRect
    {
        #region Properties

        /// <summary>
        /// The top left of this rectangle
        /// </summary>
        private Tuple<double,double> topLeft;
        /// <summary>
        /// The bottom right of this rectangle
        /// </summary>
        private Tuple<double,double> bottomRight;

        /// <summary>
        /// Gets the top left of this rectangle
        /// </summary>
        public Tuple<double,double> TopLeft
        {
            get { return topLeft; }
            set { topLeft = value; }
        }

        /// <summary>
        /// Gets the top right of this rectangle
        /// </summary>
        public Tuple<double,double> TopRight
        {
            get { return new Tuple<double,double>(bottomRight.Item1, topLeft.Item2); }
            set
            {
                bottomRight = new Tuple<double, double>(value.Item1, bottomRight.Item2);
                topLeft = new Tuple<double, double>(topLeft.Item1, value.Item2);
                //bottomRight.X = value.X;
                //topLeft.Y = value.Y;
            }
        }

        /// <summary>
        /// Gets the bottom right of this rectangle
        /// </summary>
        public Tuple<double, double> BottomRight
        {
            get { return bottomRight; }
            set { bottomRight = value; }
        }

        /// <summary>
        /// Gets the bottom left of this rectangle
        /// </summary>
        public Tuple<double, double> BottomLeft
        {
            get { return new Tuple<double, double>(topLeft.Item1, bottomRight.Item2); }
            set
            {
                topLeft = new Tuple<double, double>(value.Item1, topLeft.Item2);
                bottomRight = new Tuple<double, double>(bottomRight.Item1, value.Item2);
                //topLeft.X = value.X;
                //bottomRight.Y = value.Y;
            }
        }

        /// <summary>
        /// Gets the top of this rectangle
        /// </summary>
        public double Top
        {
            get { return TopLeft.Item2; }
            set 
            {
                topLeft = new Tuple<double, double>(topLeft.Item1, value);
                //topLeft.Y = value; 
            }
        }

        /// <summary>
        /// Gets the left of this rectangle
        /// </summary>
        public double Left
        {
            get { return TopLeft.Item1; }
            set 
            {
                topLeft = new Tuple<double, double>(value, topLeft.Item2);
                //topLeft.X = value; 
            }
        }

        /// <summary>
        /// Gets the bottom of this rectangle
        /// </summary>
        public double Bottom
        {
            get { return BottomRight.Item2; }
            set 
            {
                bottomRight = new Tuple<double, double>(bottomRight.Item1, value);
                //bottomRight.Y = value; 
            }
        }

        /// <summary>
        /// Gets the right of this rectangle
        /// </summary>
        public double Right
        {
            get { return BottomRight.Item1; }
            set 
            {
                bottomRight = new Tuple<double, double>(value, bottomRight.Item2);
                //bottomRight.X = value; 
            }
        }

        /// <summary>
        /// Gets the width of this rectangle
        /// </summary>
        public double Width
        {
            get { return bottomRight.Item1 - topLeft.Item1; }
        }

        /// <summary>
        /// Gets the height of this rectangle
        /// </summary>
        public double Height
        {
            get { return bottomRight.Item2 - topLeft.Item2; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Floating-point rectangle constructor
        /// </summary>
        /// <param name="topleft">The top left point of the rectangle</param>
        /// <param name="bottomright">The bottom right point of the rectangle</param>
        public FRect(Tuple<double, double> topleft, Tuple<double, double> bottomright)
        {
            topLeft = topleft;
            bottomRight = bottomright;
        }

        /// <summary>
        /// Floating-point rectangle constructor
        /// </summary>
        /// <param name="top">The top of the rectangle</param>
        /// <param name="left">The left of the rectangle</param>
        /// <param name="bottom">The bottom of the rectangle</param>
        /// <param name="right">The right of the rectangle</param>
        public FRect(double top, double left, double bottom, double right)
        {
            topLeft = new Tuple<double, double>(left, top);
            bottomRight = new Tuple<double, double>(right, bottom);
        }

        #endregion

        #region Intersection testing functions

        /// <summary>
        /// Checks if this rectangle contains a point
        /// </summary>
        /// <param name="Point">The point to test</param>
        /// <returns>Whether or not this rectangle contains the point</returns>
        public bool Contains(Tuple<double, double> Point)
        {
            return (topLeft.Item1 <= Point.Item1 && bottomRight.Item1 >= Point.Item1 &&
                    topLeft.Item2 <= Point.Item2 && bottomRight.Item2 >= Point.Item2);
        }

        /// <summary>
        /// Checks if this rectangle intersects another rectangle
        /// </summary>
        /// <param name="Rect">The rectangle to check</param>
        /// <returns>Whether or not this rectangle intersects the other</returns>
        public bool Intersects(FRect Rect)
        {
            return (!( Bottom < Rect.Top ||
                       Top > Rect.Bottom ||
                       Right < Rect.Left ||
                       Left > Rect.Right ));
        }

        public void draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Out.WriteLine("Left: " + Left + " Top: " + Top + " W: " + Width + " H: " + Height);
            Out.WriteLine("TopLeft " + TopLeft + " BottomRight " + BottomRight);
            g.DrawRectangle(pen, (float)Left, (float)Top, (float)Width, (float)Height);
        }

        #endregion
    }
}
