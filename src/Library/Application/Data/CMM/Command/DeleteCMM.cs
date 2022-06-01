using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.CMM.Command
{
    public class DeleteCMM : IRequest
    {
        public string Id { get; set; }
    }
    public class DeleteCMMHandler : IRequestHandler<DeleteCMM>
    {
        private readonly ICMMRepository cmmRepository;

        public DeleteCMMHandler(ICMMRepository cmmRepository)
        {
            this.cmmRepository = cmmRepository;
        }

        public Task<Unit> Handle(DeleteCMM request, CancellationToken cancellationToken)
        {
            cmmRepository.Delete(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }

}
