namespace com.azi
{
    public class Fraction
    {
        private readonly int numenator;
        private readonly int denominator;

        public Fraction(int n, int d)
        {
            numenator = n;
            denominator = d;
        }

        public float ToFloat() => numenator / (float)denominator;

        public override string ToString() => numenator + "/" + denominator;
    }
}