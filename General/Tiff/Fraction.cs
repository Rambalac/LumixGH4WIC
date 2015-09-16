namespace com.azi
{
    public class Fraction
    {
        readonly int numenator;
        readonly int denominator;

        public Fraction(int n, int d)
        {
            numenator = n;
            denominator = d;
        }

        public float ToFloat() => numenator / (float)denominator;

        public override string ToString() => numenator + "/" + denominator;
    }
}