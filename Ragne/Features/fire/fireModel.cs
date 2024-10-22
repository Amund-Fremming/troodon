using System;

namespace Ragne.Features.fire; 

public class fireModel
{
    public Guid Id { get; set; }

    public fireModel()
    {
        Id = Guid.NewGuid();
    }
}
