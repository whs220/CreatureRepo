// Will Slyman
// 1/27/2023
// Class: Conflict
// Holds information about the conflict of the random story!!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_RandomStory
{
    public enum ending
    {
        Happy,
        Tragic,
        Twist,
        Cliffhanger,
        Strange,
        Lame
    }

    internal class Conflict
    {
        //  Fields
        private string conflict;
        private ending end;
        

        /// <summary>
        /// Parameterized constructor to make a conflict!
        /// </summary>
        /// <param name="conflict">String of the conflict!</param>
        /// <param name="end">Type of ending to append!</param>
        public Conflict(string conflict, ending end)
        {
            this.conflict = conflict;
            this.end = end;
        }
    }
}
