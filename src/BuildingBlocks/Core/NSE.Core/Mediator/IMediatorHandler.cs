using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T command) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}