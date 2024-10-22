using System;

namespace Ragne.Features.seks; 

public class seksModel
{
    public Guid Id { get; set; }

    public seksModel()
    {
        Id = Guid.NewGuid();
    }
}
