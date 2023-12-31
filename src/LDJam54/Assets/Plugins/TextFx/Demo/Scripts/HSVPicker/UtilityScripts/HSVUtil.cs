﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace HSVPickerDemo
{
    #region ColorUtilities

    public static class HSVUtil
    {

        public static HsvColor ConvertRgbToHsv(Color color)
        {
            return ConvertRgbToHsv((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
        }

        // Converts an RGB color to an HSV color.
        public static HsvColor ConvertRgbToHsv(double r, double b, double g)
        {

            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(r, g), b);
            v = Math.Max(Math.Max(r, g), b);
            delta = v - min;

            if (v == 0.0)
            {
                s = 0;

            }
            else
                s = delta / v;

            if (s == 0)
                h = 0.0f;

            else
            {
                if (r == v)
                    h = (g - b) / delta;
                else if (g == v)
                    h = 2 + (b - r) / delta;
                else if (b == v)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;

            }

            HsvColor hsvColor = new HsvColor();
            hsvColor.H = h;
            hsvColor.S = s;
            hsvColor.V = v / 255;

            return hsvColor;

        }

        public static Color ConvertHsvToRgb(HsvColor color)
        {
            return ConvertHsvToRgb(color.H, color.S, color.V);
        }

        // Converts an HSV color to an RGB color.
        public static Color ConvertHsvToRgb(double h, double s, double v)
        {

            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;


                if (h == 360)
                    h = 0;
                else
                    h = h / 60;

                i = (int)(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0f - f)));

                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }

            }



            return new Color((float)r, (float)g, (float)b, 1);//255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));

        }

        // Generates a list of colors with hues ranging from 0 360
        // and a saturation and value of 1. 
        public static List<Color> GenerateHsvSpectrum(int minHue = 0, int maxHue = 360)
        {

            List<Color> colorsList = new List<Color>(8);


            //for (int i = 0; i < 29; i++)
            //{

            //    colorsList.Add(
            //        ConvertHsvToRgb(i * 12, 1, 1)

            //    );

            //}

            for (int i = minHue; i < (maxHue - 1); i++)
            {

                colorsList.Add(
                    ConvertHsvToRgb(i, 1, 1)

                );

            }

			colorsList.Add(ConvertHsvToRgb(maxHue, 1, 1));

            return colorsList;

        }

        public static Texture2D GenerateHSVTexture(int width, int height, int minHue = 0, int maxHue = 360)
        {
            var list = GenerateHsvSpectrum(minHue, maxHue);

            float interval = (float)list.Count / height;

            var texture = new Texture2D(width, height);

            int ySize = Mathf.Max(1,(int)(1f/(list.Count/interval) * height));

            int colorH = 0;

            Color color = Color.white;
            for (float cnt = 0; cnt < list.Count; cnt += interval)
            {
                color = list[(int)cnt];
                Color[] colors = new Color[width *ySize];
                for (int i = 0; i < width * ySize; i++) 
                {
                    colors[i] = color;
                }
                if (colorH < height)
                {
                    texture.SetPixels(0, colorH, width, ySize, colors);
                }
                colorH += ySize;
            }

            texture.Apply();

            return texture;
        }


        public static Texture2D GenerateColorTexture(Color mainColor, Texture2D texture, int minHue = 0, int maxHue = 360, float minSat = 0f, float maxSat = 1f, float minV = 0f, float maxV = 1f)
        {
            int width = texture.width;
            int height = texture.height;

            var hsvColor = ConvertRgbToHsv(mainColor);
			var realH = (minHue + maxHue) - (360f - hsvColor.H);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var adjustedColor = hsvColor;
                    adjustedColor.V = Mathf.Clamp((float)y / height, minV, maxV);
                    adjustedColor.S = Mathf.Clamp((float)x / width, minSat, maxSat);
                    var color = ConvertHsvToRgb(realH, adjustedColor.S, adjustedColor.V);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();

            return texture;
        }

        public static Texture2D GenerateColorTexture(int width, int height, Color mainColor, int minHue = 0, int maxHue = 360, float minSat = 0f, float maxSat = 1f, float minV = 0f, float maxV = 1f)
        {
            return GenerateColorTexture(mainColor, new Texture2D(width, height), minHue, maxHue, minSat, maxSat, minV, maxV);
        }

        
    }

    #endregion ColorUtilities


    // Describes a color in terms of
    // Hue, Saturation, and Value (brightness)
    #region HsvColor
    public struct HsvColor
    {

        public double H;
        public double S;
        public double V;

        public HsvColor(double h, double s, double v)
        {
            this.H = h;
            this.S = s;
            this.V = v;

        }

        public override string ToString()
        {
            return "{"+H+","+S+","+V+"}";
        }
    }
    #endregion HsvColor
}