namespace com.azi.Filters
{
    interface IIndependentComponentFilter
    {
        object CreateOutputCurve(int length);
        object CreateInputCurve(int length);
        void ProcessCurve(object incurve, object outcurve);
    }
}
