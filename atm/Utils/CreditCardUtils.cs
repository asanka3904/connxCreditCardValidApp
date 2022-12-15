using atm.Enum;

namespace atm.Utils
{
    public static class CreditCardUtils
    {
        public static bool LuhnAlgorithm(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }


        public static CardType Checkcardtype(string cardNo)
        {
            if (cardNo.Length < 10)
            {
                return CardType.UNKNOWN;
            }
             else  if (cardNo.Substring(0, 2) == "37" || cardNo.Substring(0, 2) == "34" && cardNo.Length == 15)
            {
                return CardType.AMEX;
            } else if ( cardNo.Substring(0, 4) == "6011" && cardNo.Length == 15)
            {
                return CardType.DISCOVER;
            }
            else if (new String[] { "51", "52", "53", "54", "55" }.Contains(cardNo.Substring(0, 2))  && cardNo.Length == 16)
            {
                
                return CardType.MASTERCARD;
            }
            else if (cardNo.Substring(0, 1)=="4" && cardNo.Length == 16 || cardNo.Length ==  13)
            {

                return CardType.VISA;
            }
            else
            {
                return CardType.UNKNOWN;
            }
        }
    }
}
