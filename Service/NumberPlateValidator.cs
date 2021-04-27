using System;
using System.Linq;

namespace Service
{
    public interface INumberPlateValidator
    {
        bool Validate(string numberPlate);
    }
    public class NumberPlateValidator: INumberPlateValidator
    {
        public bool Validate(string numberPlate)
        {
            return numberPlate.All(Char.IsLetterOrDigit);
        }
    }
}
