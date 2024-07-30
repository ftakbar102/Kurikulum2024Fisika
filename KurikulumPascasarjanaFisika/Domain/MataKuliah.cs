namespace KurikulumPascasarjanaFisika.Domain
{
	public class MataKuliah
	{
		public string Kode { get; set; } = string.Empty;
		public string NamaId { get; set; } = string.Empty;
		public string NamaEn { get; set; } = string.Empty;
		public string Kategori { get; set; } = string.Empty;
		public JenisMataKuliah Jenis { get; set; }
		public int Sks { get; set; }
		public string? Metode { get; set; } = string.Empty;
		public string? Penilaian { get; set; } = string.Empty;
        public string? Modalitas { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
		public string ProgramStudi { get; set; } = string.Empty;

	}

}
