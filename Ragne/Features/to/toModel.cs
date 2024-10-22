using System;

namespace Ragne.Features.to; 

public class toModel
{
    public Guid Id { get; set; }

    public toModel()
    {
        Id = Guid.NewGuid();
    }
}
