using KurikulumPascasarjanaFisika.Domain;

namespace KurikulumPascasarjanaFisika.Services
{
    public interface IKatalogService
    {
        Task<IEnumerable<MataKuliah>?> GetKatalog(string programStudi);
        Task<MataKuliah?> GetMataKuliah(string kodeMK, string programStudi);
        Task<Dictionary<string, int>?> GetMataKuliahByJenis(string programStudi);
    }
}
