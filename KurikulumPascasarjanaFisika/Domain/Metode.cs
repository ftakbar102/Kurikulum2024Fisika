namespace KurikulumPascasarjanaFisika.Domain
{
	public class Metode
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

            Metode? metode = obj as Metode;

            return metode is not null && metode.Name == this.Name;
        }
    }
}
