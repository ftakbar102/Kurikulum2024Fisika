namespace KurikulumPascasarjanaFisika.Domain
{
    public class Modalitas
    {
        public string Name { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            Modalitas? modalitas = obj as Modalitas;

            return modalitas is not null && modalitas.Name == this.Name;
        }
    }
}
