namespace KurikulumPascasarjanaFisika.Domain
{
	public class CapaianLulusan : IComparable<CapaianLulusan>
	{
		public string KodeCPL { get; set; } = string.Empty;
		public string UnsurCPL { get; set; } = string.Empty;
		public string UnsurCPLEn { get; set; } = string.Empty;

		public int CompareTo(CapaianLulusan? other)
		{
			return other is null ? -1 : string.Compare(KodeCPL, other.KodeCPL);
		}
	}
}
