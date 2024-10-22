using System;

namespace Ragne.Features.en; 

public class enModel
{
    public Guid Id { get; set; }

    public enModel()
    {
        Id = Guid.NewGuid();
    }
}
