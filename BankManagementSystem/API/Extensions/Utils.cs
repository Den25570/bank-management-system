namespace API.Extensions
{
    public static class Utils
    {
        public static int GetDepositMainAccountCode(this int depositType)
        {
            switch(depositType) {
                case 1: return 3404;
                case 2: return 3414;
                default: throw new ArgumentException("Invalid deposit type");
            }
        }

        public static int GetDepositPercentAccountCode(this int depositType)
        {
            switch (depositType)
            {
                case 1: return 3470;
                case 2: return 3471;
                default: throw new ArgumentException("Invalid deposit type");
            }
        }

        public static int GetCreditMainAccountCode(this int creditCode)
        {
            return creditCode;
        }

        public static int GetCreditPercentAccountCode(this int creditCode)
        {
            switch (creditCode)
            {
                case 2412: return 2471;
                case 2413: return 2472;
                case 2414: return 2472;
                case 2415: return 2472;
                case 2421: return 2476;
                case 2422: return 2477;
                case 2423: return 2478;
                case 2424: return 2478;
                case 2426: return 2478;
                case 2427: return 2475;
                case 2428: return 2478;
                case 2429: return 2478;
                default: throw new ArgumentException("Invalid deposit type");
            }
        }

        public static int ReverseInt(this int num)
        {
            int result = 0;
            while (num > 0)
            {
                result = result * 10 + num % 10;
                num /= 10;
            }
            return result;
        }
    }
}
