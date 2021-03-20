//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sloths.source.model
{
    public readonly struct Color
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }
        //[JsonConstructor]
        private Color(byte R, byte G, byte B, byte A) { this.R = R; this.G = G; this.B = B; this.A = A; }
        public static Color FromRGBA(byte R, byte G, byte B, byte A) { return new Color(R, G, B, A); }
        public static Color FromRGBA(uint rgba) { return new Color((byte)(rgba & 0xff), (byte)((rgba >> 8) & 0xff), (byte)((rgba >> 16) & 0xff), (byte)((rgba >> 24) & 0xff)); }
        public static Color FromARGB(uint argb) { return new Color((byte)((argb >> 8) & 0xff), (byte)((argb >> 16) & 0xff), (byte)((argb >> 24) & 0xff), (byte)(argb & 0xff)); }

        public uint RGBA() => (R | (((uint)(G)) << 8)) | (((uint)(B)) << 16) | (((uint)(A)) << 24);
        public uint ARGB() => (A | (((uint)(R)) << 8)) | (((uint)(G)) << 16) | (((uint)(B)) << 24);

        public static Color BLACK = new Color(0, 0, 0, 255);
        public static Color BLUE = new Color(0, 0, 180, 255);
        public static Color GREEN = new Color(0, 128, 0, 255);
        public static Color CYAN = new Color(0, 128, 128, 255);
        public static Color RED = new Color(128, 0, 0, 255);
        public static Color MAGENTA = new Color(128, 0, 128, 255);
        public static Color BROWN = new Color(128, 128, 0, 255);
        public static Color LIGHTGRAY = new Color(192, 192, 192, 255);
        public static Color DARKGRAY = new Color(128, 128, 128, 255);
        public static Color LIGHTBLUE = new Color(0, 0, 255, 255);
        public static Color LIGHTGREEN = new Color(0, 255, 0, 255);
        public static Color LIGHTCYAN = new Color(0, 255, 255, 255);
        public static Color LIGHTRED = new Color(255, 0, 0, 255);
        public static Color LIGHTMAGENTA = new Color(255, 0, 255, 255);
        public static Color YELLOW = new Color(255, 255, 0, 255);
        public static Color WHITE = new Color(255, 255, 255, 255);
        public static Color DARKGRAYTRANSPARENT = new Color(128, 128, 128, 128);
    }
}
