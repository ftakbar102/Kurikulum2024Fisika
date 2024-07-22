namespace KurikulumPascasarjanaFisika.Domain
{
	public class RPMK
	{
		public MataKuliah MataKuliah { get; set; } = new();
		public IEnumerable<Kajian> BahanKajian { get; set; } = [];
		public IEnumerable<CapaianMataKuliah> CPMK { get; set; } = [];
		public IEnumerable<RelasiMataKuliah> Relasi { get; set; } = [];
		
	}
}
