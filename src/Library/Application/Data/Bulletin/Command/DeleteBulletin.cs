using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Bulletin.Command
{
    public class DeleteBulletin : IRequest
    {
        public string Id { get; set; }
    }
    public class DeleteBulletinHandler : IRequestHandler<DeleteBulletin>
    {
        private readonly IBulletinRepository bulletinRepository;

        public DeleteBulletinHandler(IBulletinRepository bulletinRepository)
        {
            this.bulletinRepository = bulletinRepository;
        }

        public Task<Unit> Handle(DeleteBulletin request, CancellationToken cancellationToken)
        {
            bulletinRepository.Delete(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }

}
