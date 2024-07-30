using KurikulumPascasarjanaFisika.Domain;

namespace KurikulumPascasarjanaFisika.Services
{
    public interface ICapaianService
    {
        Task<Dictionary<MataKuliah, IEnumerable<bool>>?> GetCapaianMataKuliahMapping(string programStudi);
        Task<Dictionary<string, int>?> JumlahMataKuliahPerCapaian(string programStudi, KategoriMataKuliah kategori = KategoriMataKuliah.Semua);
        Task<IEnumerable<CapaianLulusan>?> GetCPLProdi(string programStudi);
        Task<IEnumerable<CapaianMataKuliah>?> GetCPMK(string kodeMK, string programStudi);
    }
}