using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcApp.Areas.BlindLuck.Models
{
    public class Die
    {
        public static Die New() => new Die();

        private static Random RnD = new Random();
        public int FaceValue { get; private set; }
        public void Roll()
        {
            FaceValue = RnD.Next(1, 7);
        }
        public Die()
        {
            Roll();
        }
    }
}