using LockerAPI.DTOs;
using LockerAPI.DTOs.CreateUpdateObjects;
using LockerAPI.DTOs.PatchObject;
namespace LockerAPI.Repositories
{
    public interface ICompanysRepository
    {
        public Task<IEnumerable<Company>> GetCompanysAsync();

        public Task<Company> GetCompanyByIdAsync(Guid id);

        public Task CreateCompanyAsync(Company company);

        public Task<bool> DeleteCompanyAsync(Guid id);

        public Task<CreateUpdateCompany> UpdateCompanyAsync(Guid id, CreateUpdateCompany company);

        public Task<PatchCompany> UpdatePartiallyCompanyAsync(Guid id, PatchCompany company);


    }
}
