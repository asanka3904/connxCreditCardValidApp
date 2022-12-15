using atm.Models;

namespace atm.Repository
{
    public interface ICreditCard
    {
     List<CreditCardModel>  checkCard(List<string> cardNo);
    }
}
