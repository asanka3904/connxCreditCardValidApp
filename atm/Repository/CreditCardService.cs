using atm.Enum;
using atm.Models;
using atm.Utils;
using Microsoft.VisualBasic;

namespace atm.Repository
{
    public class CreditCardService : ICreditCard
    {
        public List<CreditCardModel> checkCard(List<string> cardNos)
        {
            var carlist=new List<CreditCardModel>();

            for (int i = 0; i < cardNos.Count; i++)
            {
                var cardmodel = new CreditCardModel();
                var card = cardNos[i];

                var status= CreditCardUtils.LuhnAlgorithm(card);
                cardmodel.cardStatus = status;

                var type= CreditCardUtils.Checkcardtype(card);

                cardmodel.cardType = type;
                cardmodel.cardNo = card;

                carlist.Add(cardmodel);

            }

            return carlist;
        }
    }
}
