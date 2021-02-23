using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneBorder
{
    class Math2
    {
        public enum RoundTechnique
        {
            ToNearest = 0,
            Down = 1,
            Up = 2,
        }

        ///<summary>Rounds value (Example: Round(0.3, 0.5, RoundTechnique.Up) will round 0.3 up to the nearest multiple of 0.5) (NOTE: This hasn't been thoroughly tested))</summary>
        ///<param name="value">Value to round</param>
        ///<param name="multipleOf">This value's multiples are used as a base in which to round to</param>
        ///<param name="roundTechnique">Technique used for rounding (ToNearest, Down, or Up)</param>
        ///<returns>Rounded value</returns>
        public static double Round(double value, double multipleOf, RoundTechnique roundTechnique)
        {
            return ((roundTechnique == Math2.RoundTechnique.ToNearest) ? (Math.Round(value / multipleOf) * multipleOf) : (
                (roundTechnique == Math2.RoundTechnique.Down) ? (Math.Floor(value / multipleOf) * multipleOf) :
                (Math.Ceiling(value / multipleOf) * multipleOf)
                ));
        }
    }
}
