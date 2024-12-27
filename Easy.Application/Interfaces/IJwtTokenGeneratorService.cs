using Easy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Application.Interfaces
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(Usuario usuario);
    }
}
