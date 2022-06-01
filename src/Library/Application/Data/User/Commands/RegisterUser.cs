using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.User.Commands
{
    public class RegisterUser : IRequest<Library.Domain.Entities.User> 
    {
        public string Email { get; set; }
        public string DockSiteToken { get; set; }
    }
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Library.Domain.Entities.User>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Library.Domain.Entities.User> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var entity = _userRepository.GetByUsername(request.Email);
            if(entity != null)
            {
                return Task.FromResult(entity);
            }
            var user = new Library.Domain.Entities.User();
            user.Username = request.Email;
            user.Roles = new Domain.Entities.Roles("enabled", new[] { "Interanl" }, null);
            user.Emails = new List<Domain.Entities.Emails>()
            {
                new Domain.Entities.Emails(request.Email,true)
            };
            var u = _userRepository.Add(user);
            return Task.FromResult(u);
        }
    }
}
