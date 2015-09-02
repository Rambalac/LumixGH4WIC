namespace com.azi.Filters
{
    public interface IColorToColorFilter<in TA, in TB> : IFilter
    {
        void ProcessColor(TA[] input, int inputOffset, TB[] output, int outputOffset);
    }
}