using LockerAPI.Repositories;
using LockerAPI.DTOs;
using LockerAPI.DataContext;
using LockerAPI.Helpers;
using LockerAPI.DTOs.CreateUpdateObjects;
using LockerAPI.DTOs.PatchObject;


namespace LockerAPI.Services
{
    public class CompanysService : ICompanysService
    {
        private readonly ICompanysRepository _repository;

        public CompanysService(ICompanysRepository repository)
        {
            _repository = repository;
        }

        public async Task<Company> GetCompanysByIdAsync(Guid id)
        {
            return await _repository.GetCompanyByIdAsync(id);
        }

        public async Task<IEnumerable<Company>> GetCompanysAsync()
        {
            return await _repository.GetCompanysAsync();
        }

        public async Task CreateCompanyAsync(Company newCompany)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(newCompany.dateadded, newCompany.dateadded);
            await _repository.CreateCompanyAsync(newCompany);
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            return await _repository.DeleteCompanyAsync(id);
        }
        public async Task<CreateUpdateCompany> UpdateCompanyAsync(Guid id, CreateUpdateCompany company)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(company.dateadded, company.dateadded);
            return await _repository.UpdateCompanyAsync(id, company);
        }
        public async Task<PatchCompany> UpdatePartiallyCompanyAsync(Guid id, PatchCompany company)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(company.dateadded, company.dateadded);
            return await _repository.UpdatePartiallyCompanyAsync(id, company);
        }
    }
}
