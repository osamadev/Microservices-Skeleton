using System.Threading.Tasks;
using RawRabbit.Context;

namespace Actio.Common.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
          Task HandleAsync(T @event, IMessageContext messageContext);
    }
}