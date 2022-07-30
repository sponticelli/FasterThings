using System;
using UnityEngine;

namespace LiteNinja.MathUtils
{
    public static class MathHelper
    {
        public static double Angle(double cx, double cy, double px, double py)
        {
            var num = 180.0 / Math.PI * Math.Atan2(py - cy, px - cx);
            if (num < 0.0)
                num += 360.0;
            return num;
        }

        public static double Angle(Vector2 center, Vector2 point)
        {
            return Angle(center.x, center.y, point.x, point.y);
        }
    }
}