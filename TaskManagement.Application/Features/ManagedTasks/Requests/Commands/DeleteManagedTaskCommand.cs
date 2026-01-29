using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Features.ManagedTasks.Requests.Commands
{
    public class DeleteManagedTaskCommand:IRequest  
    {
        public int Id { get; set; }
    }
}
