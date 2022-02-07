namespace API.Extensions
{
    public static class Utils
    {
        public static int GetMainAccountCode(this int depositType)
        {
            switch(depositType) {
                case 1: return 3404;
                case 2: return 3414;
                default: throw new ArgumentException("Invalid deposit type");
            }
        }

        public static int GetPercentAccountCode(this int depositType)
        {
            switch (depositType)
            {
                case 1: return 3470;
                case 2: return 3471;
                default: throw new ArgumentException("Invalid deposit type");
            }
        }
    }
}
