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
    /// <summary>
    /// Enum that holds the possible endings
    /// </summary>
    public enum Ending
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
        private string dialouge;
        private Ending end;

        /// <summary>
        /// Parameterized constructor to make a conflict!
        /// </summary>
        /// <param name="conflict">String of the conflict!</param>
        /// <param name="end">Type of ending to append!</param>
        public Conflict(string dialouge, Ending end)
        {
            this.dialouge = dialouge;
            this.end = end;
        }

        /// <summary>
        /// Get for dialouge
        /// </summary>
        public string Dialouge
        {
            get
            {
                return dialouge;
            }
        }

        /// <summary>
        /// Get for ending
        /// </summary>
        public Ending End
        {
            get
            {
                return end;
            }
        }
    }
}
