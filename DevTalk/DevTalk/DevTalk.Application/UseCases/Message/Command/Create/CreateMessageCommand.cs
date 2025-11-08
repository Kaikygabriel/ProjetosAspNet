using MediatR;

namespace DevTalk.Application.UseCases.Message.Command.Create;

public class CreateMessageCommand(Domain.BackOffice.Entities.Message message) : IRequest<bool>;