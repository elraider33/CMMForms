using Library.Application.Models;
using Library.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.User.Commands
{
    public class AuthUser : IRequest<AuthModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AuthUserHandler : IRequestHandler<AuthUser, AuthModel>
    {
        private readonly IAuthService _authService;

        public AuthUserHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<AuthModel> Handle(AuthUser request, CancellationToken cancellationToken)
        {
            var auth = _authService.Authenticate(request.Username, request.Password);
            return Task.FromResult(auth);
        }
    }
}
