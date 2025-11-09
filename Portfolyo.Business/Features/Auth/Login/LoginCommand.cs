using ErrorOr;
using MediatR;

namespace Portfolyo.Business.Features.Auth.Login
{
    public sealed record LoginCommand(string UserNameOrEmail,
        string Password):IRequest<LoginCommandResponse>;
}
