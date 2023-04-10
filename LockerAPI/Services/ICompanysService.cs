using LockerAPI.Services;
using LockerAPI.DTOs;
using LockerAPI.DTOs.CreateUpdateObjects;
using LockerAPI.DTOs.PatchObject;

namespace LockerAPI.Services
{
    public interface ICompanysService
    {
        public Task<IEnumerable<Company>> GetCompanysAsync();
        public Task<Company> GetCompanysByIdAsync(Guid id);

        public Task CreateCompanyAsync(Company newCompany);

        public Task<bool> DeleteCompanyAsync(Guid id);
        public Task<CreateUpdateCompany> UpdateCompanyAsync(Guid id, CreateUpdateCompany company);

        public Task<PatchCompany> UpdatePartiallyCompanyAsync(Guid id, PatchCompany company);
    }
}
