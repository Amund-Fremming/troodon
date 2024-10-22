using System;

namespace Ragne.Features.fem; 

public class femModel
{
    public Guid Id { get; set; }

    public femModel()
    {
        Id = Guid.NewGuid();
    }
}
