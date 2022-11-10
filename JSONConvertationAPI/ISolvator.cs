namespace JSONConvertationAPI
{
    public interface ISolvator
    {
        Messages.CalcOutputMessage Solve(Messages.CalcInputMessage Input);
    }
}
