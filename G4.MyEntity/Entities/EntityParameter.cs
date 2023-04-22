namespace G4.MyEntity.Entities {
    internal class EntityParameter {

        public string Name { get; set; }
        public object? Value { get; set; }

        public EntityParameter(string name, object? value) {
            Name = name;
            Value = value;
        }

    }
}
