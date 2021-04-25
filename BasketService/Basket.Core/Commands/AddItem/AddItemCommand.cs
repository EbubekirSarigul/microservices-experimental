using Basket.Core.Models;
using MicroserviceTraining.Framework.Commands;

namespace Basket.Core.Commands.AddItem
{
    public class AddItemCommand : BaseCommand<AddItemResult>
    {
        public string PlayerId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
