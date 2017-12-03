using System.Drawing;

namespace Di
{
    public class TagStatistic
    {
        public double Coefficient { get; }
        public string Value { get; }

        public TagStatistic(string value, double coefficient)
        {
            this.Value = value;
            Coefficient = coefficient;
        }
    }
}