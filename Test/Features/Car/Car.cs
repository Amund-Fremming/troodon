
            using System;

            namespace troodon.Cli
            {
                public class Car
                {
                    public Guid Id { get; set; }

                    public Car()
                    {
                        Id = Guid.NewGuid();
                    }
                }
            }