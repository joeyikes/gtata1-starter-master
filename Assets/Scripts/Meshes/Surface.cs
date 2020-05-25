using System;
using UnityEngine;

namespace Meshes
{
    public  abstract class Surface : MonoBehaviour
    {
        private int subDivX;
        private int subDivY;
        private float u;
        private float v;
        private float umin;
        private float umax;
        private float vmin;
        private float vmax;
        private bool isClockWiseWindingOrder = false;
        public Surface()
        {
            
        }
        
        public float GetNewV(float oldv)
        {
            if (vmin < 0)
            {
                this.v = (oldv * (vmax*2)) - vmax;
            }

            if (vmin == 0)
            {
                this.v = oldv * vmax;
            }
            else
            {
                this.v = (oldv * (vmax - vmin)) + vmin;
            }
            return v;
        }

        public float GetNewU(float oldu)
        {
            if (umin < 0)
            {
                this.u = (oldu * (umax*2)) - umax;
            }

            if (umin == 0)
            {
                this.u = oldu * umax;
            }
            else
            {
                this.u = (oldu * (umax - umin)) + umin;
            }
            return u;
        }

        public void SetURange(float min, float max)
        {
            this.umin = min;
            this.umax = max;
            
        }
        public void SetVRange(float min, float max)
        {
            this.vmin = min;
            this.vmax = max;
            
        }
        public void SetSubDivisions(int x, int y)
        {
            subDivX = x;
            subDivY = y;
        }

        public int GetSubX()
        {
            return subDivX;
        }
        public int GetSubY()
        {
            return subDivY;
        }

        public void SetWindingOrder(bool wo)
        {
            isClockWiseWindingOrder = wo;
        }

        public bool IsClockWiseWindingOrder()
        {
            return isClockWiseWindingOrder;
        }
        public abstract Vector3 GetParamFunc(float u,float v);
    }
}