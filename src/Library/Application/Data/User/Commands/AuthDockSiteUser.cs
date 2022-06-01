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
    public class AuthDockSiteUser : IRequest<AuthModel>
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
    public class AuthDockSiteUserHandler : IRequestHandler<AuthDockSiteUser, AuthModel>
    {
        private readonly IAuthService _authService;

        public AuthDockSiteUserHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<AuthModel> Handle(AuthDockSiteUser request, CancellationToken cancellationToken)
        {
            var auth = _authService.AuthenticateWithDocksite(request.Username, request.Token);
            return Task.FromResult(auth);
        }
    }
}
