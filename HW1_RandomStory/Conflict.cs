using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_RandomStory
{
    internal class Conflict
    {
        //  Fields
        private string conflict;
        private string resolution;
        private ending end;
        public enum ending
        {
            Happy,
            Tragic,
            Twist,
            Cliffhanger,
            Strange,
            Lame
        }

        /// <summary>
        /// Parameterized constructor to make a conflict!
        /// </summary>
        /// <param name="conflict">String of the conflict!</param>
        /// <param name="resolution">String of the resolution!</param>
        /// <param name="end">Type of ending to append!</param>
        public Conflict(string conflict, string resolution, ending end)
        {
            this.conflict = conflict;
            this.resolution = resolution;
            this.end = end;
        }

        /// <summary>
        /// Returns the conflict and resolution
        /// </summary>
        /// <returns></returns>
        public string[] GetConflict()
        {
            string[] data = { conflict, GetEnding() };
            return data;
        }

        private string GetEnding()
        {
            switch (end)
            {
                case 0:
                    return "Happy";
            }

            return "";
        }
    }
}
