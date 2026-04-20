using System;
using System.Collections.Generic;
using System.Text;

namespace RailFlow.TrainService.Domain
{
    public class Train
    {
        public Guid Id { get; private set; }
        public string Number { get; private set; }

        private Train() { }

        public Train(string number) {
            Id = Guid.NewGuid();
            Number = number;
        }
    }
}
