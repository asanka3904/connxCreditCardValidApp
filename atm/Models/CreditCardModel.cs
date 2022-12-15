using atm.Enum;

namespace atm.Models
{
    public class CreditCardModel
    {
        public CardType cardType { get; set; }
        public string cardNo { get; set; }

        public bool cardStatus { get; set; }

    }
}
