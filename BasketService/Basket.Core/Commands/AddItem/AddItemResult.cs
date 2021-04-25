using Basket.Core.Models;
using MicroserviceTraining.Framework.Models;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemResult : BaseResult
    {
        public PlayerBasket Basket { get; set; }
    }
}
