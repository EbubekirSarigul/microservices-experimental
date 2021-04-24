using Basket.Core.Models;
using MicroserviceTraining.Framework.Commands;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemCommand : BaseCommand<PlayerBasket>
    {
        public string PlayerId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
