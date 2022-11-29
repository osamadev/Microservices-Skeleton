using System.Threading.Tasks;
using RawRabbit.Context;

namespace Actio.Common.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T command, IMessageContext msgContext);
    }
}