using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicalSimulation
{
    class Circle
    {
        public double x;//position
        public double y;
        public double vx;//velocity
        public double vy;
        //Kinetic energy=1/2mv^2
        //Momentum=mv
        public Circle(double x, double y, double vx, double vy)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
        }
    }
}
