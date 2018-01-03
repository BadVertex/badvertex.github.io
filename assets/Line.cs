using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class Line
    {
        public float p1x { get; private set; }
        public float p1y { get; private set; }
        public float p2x { get; private set; }
        public float p2y { get; private set; }

        public float p1xr { get; private set; }
        public float p1yr { get; private set; }
        public float p2xr { get; private set; }
        public float p2yr { get; private set; }

        public float globalOffsetX { get; private set; }
        public float globalOffsetY { get; private set; }

        protected float a { get; private set; }
        protected float b { get; private set; }
        protected float c { get; private set; }

        protected float rotOriginX { get; private set; }
        protected float rotOriginY { get; private set; }
        protected float rotation { get; private set; }

        public Line(float p1x, float p1y, float p2x, float p2y)
        {
            this.p1x = p1x;
            this.p1y = p1y;
            this.p2x = p2x;
            this.p2y = p2y;

            rotation = 0.0f;
            this.p1xr = p1x;
            this.p1yr = p1y;
            this.p2xr = p2x;
            this.p2yr = p2y;

            globalOffsetX = 0.0f;
            globalOffsetY = 0.0f;

            recalculatePosition();
        }


        public void recalculatePosition()
        {
            a = p2yr - p1yr;
            b = p1xr - p2xr;
            c = a * (p1xr + globalOffsetX) + b * (p1yr + globalOffsetY);
        }


        public void recalculateRotation()
        {
            p1xr = (float)Math.Cos((float)rotation) * (p1x - rotOriginX) - (float)Math.Sin((float)rotation) * (p1y - rotOriginY) + rotOriginX;
            p1yr = (float)Math.Sin((float)rotation) * (p1x - rotOriginX) + (float)Math.Cos((float)rotation) * (p1y - rotOriginY) + rotOriginY;

            p2xr = (float)Math.Cos((float)rotation) * (p2x - rotOriginX) - (float)Math.Sin((float)rotation) * (p2y - rotOriginY) + rotOriginX;
            p2yr = (float)Math.Sin((float)rotation) * (p2x - rotOriginX) + (float)Math.Cos((float)rotation) * (p2y - rotOriginY) + rotOriginY;

            recalculatePosition();
        }


        public void setRotation(float x, float y, float theta)
        {
            rotation = theta % (float)(Math.PI * 2.0);
            rotOriginX = x;
            rotOriginY = y;
        }




        public static bool intersects(Line a, Line b)
        {
            float det = a.a * b.b - b.a * a.b;
            
            if (fEquals(0.0f, det))
            {
                //Console.WriteLine("No intersection");
                return false;
            }
            else
            {
                float x = (b.b * a.c - a.b * b.c) / det;
                float y = (a.a * b.c - b.a * a.c) / det;

                if (!inRange(x, Math.Min(a.p1xr, a.p2xr) + (int)a.globalOffsetX, Math.Max(a.p1xr, a.p2xr) + (int)a.globalOffsetX))
                {
                    return false;
                }
                else if (!inRange(y, Math.Min(a.p1yr, a.p2yr) + (int)a.globalOffsetY, Math.Max(a.p1yr, a.p2yr) + (int)a.globalOffsetY))
                {
                    return false;
                }
                else if (!inRange(x, Math.Min(b.p1xr, b.p2xr) + (int)b.globalOffsetX, Math.Max(b.p1xr, b.p2xr) + (int)b.globalOffsetX))
                {
                    return false;
                }
                else if (!inRange(y, Math.Min(b.p1yr, b.p2yr) + (int)b.globalOffsetY, Math.Max(b.p1yr, b.p2yr) + (int)b.globalOffsetY))
                {
                    return false;
                }

                return true;
            }
        }

        public void setGlobalOffset(float x, float y)
        {
            globalOffsetX = x;
            globalOffsetY = y;
            //recalculate();
        }

        public float length()
        {
            return (float)Math.Sqrt((p2x - p1x) * (p2x - p1x) + (p2y - p1y) * (p2y - p1y));
        }


        public static bool fEquals(float x, float y)
        {
            if (Math.Abs(x - y) < 0.0001f)
            {
                return true;
            }
            return false;
        }


        public static bool inRange(float subject, float min, float max)
        {
            if (subject >= min && subject <= max)
            {
                return true;
            }
            return false;
        }


        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Vector2(p1xr + globalOffsetX, p1yr + globalOffsetY), null, Color.White,
                         (float)Math.Atan2(p2yr - p1yr, p2xr - p1xr),
                         new Vector2(0f, 1.0f),
                         new Vector2(length(), 1f),
                         SpriteEffects.None, 0f);
        }
        

        
    }
}
