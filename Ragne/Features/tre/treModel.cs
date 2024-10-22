using System;

namespace Ragne.Features.tre; 

public class treModel
{
    public Guid Id { get; set; }

    public treModel()
    {
        Id = Guid.NewGuid();
    }
}
