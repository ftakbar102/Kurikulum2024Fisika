using KurikulumPascasarjanaFisika.Domain;

namespace KurikulumPascasarjanaFisika.Services
{
	public interface IRpmkGenerator
	{
		Task<RPMK?> GetRpmk(string kodeMK, string programStudi);

	}
}
