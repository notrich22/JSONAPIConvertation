namespace JSONConvertationAPI
{
    public static class NSSolvator
    {
        public static Messages.CalcOutputMessage Solve(Messages.CalcInputMessage Input)
        {
            int x = Input.X;
            int numberSystem = Input.NumberSystem;
            if (x <= 0)
            {
                throw new Exception("Enter an X");
            }
            string result = Convert.ToString(x, numberSystem);
            return new Messages.CalcOutputMessage(numberSystem, x, result);
        }
    }
}
