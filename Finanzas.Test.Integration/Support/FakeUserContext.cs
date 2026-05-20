using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Application.Interfaces;

namespace Finanzas.Test.Integration.Support;

public class FakeUserContext : IUserContext
{
    public Guid? UserId { get; set; } = null;
    public bool IsAuthenticated => UserId != null;
}
