using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskScheduler
{
    public static class Kernels
    {
        public static double[,] Laplacian
        {
            get
            {
                return new double[,]
                {
                     { 0,-1, 0 },
                     {-1, 4,-1 },
                     { 0,-1, 0 }
                };
            }
        }
    }
}
