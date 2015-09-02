namespace com.azi
{
    public class Fraction
    {
        private readonly int denominator;
        private readonly int numenator;

        public Fraction(int n, int d)
        {
            numenator = n;
            denominator = d;
        }

        public override string ToString()
        {
            return numenator + "/" + denominator;
        }
    }
}