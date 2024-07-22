namespace KurikulumPascasarjanaFisika.Domain
{
	public class CapaianMataKuliah
	{
		public string KodeMK { get; set; } = string.Empty;
		public string CPMK { get; set; } = string.Empty;
		public string CPMKEn { get; set; } = string.Empty;
		public string KodeCPL { get; set; } = string.Empty;
		public CapaianLulusan CPL { get; set; } = new();
	}
}
