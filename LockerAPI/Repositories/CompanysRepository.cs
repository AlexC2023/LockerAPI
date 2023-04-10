using LockerAPI.DTOs;
using LockerAPI.DTOs.CreateUpdateObjects;
using LockerAPI.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System;
using LockerAPI.DTOs.PatchObject;

namespace LockerAPI.Repositories
{
    public class CompanysRepository : ICompanysRepository
    {
        private readonly LockerDataContext _context;
        private readonly IMapper _mapper;
        //private const string name = "alexandra";
        //const -> compile time ->asignata valoarea cand declaram
        //readonly -> runtime time -> asignam valoarea in constructor


        public CompanysRepository(LockerDataContext context)
        {
            _context = context;
        }

        public CompanysRepository(LockerDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Company>> GetCompanysAsync()
        {
            return await _context.Companys.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await _context.Companys.SingleOrDefaultAsync(a => a.companyid == id);
        }
        public async Task CreateCompanyAsync(Company company)
        {
            company.companyid = Guid.NewGuid();
            _context.Companys.Add(company);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            Company company = await GetCompanyByIdAsync(id);
            if (company == null)
            {
                return false;
            }
            _context.Companys.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<CreateUpdateCompany> UpdateCompanyAsync(Guid id, CreateUpdateCompany company)
        {

            if (!await ExistCompanyAsync(id))
            {
                return null;
            }
            //exista un pachet automapper - ne ajuta sa transformam un obiect in alt obiect

        

            var companyUpdated = _mapper.Map<Company>(company);

            companyUpdated.companyid = id;

            _context.Update(companyUpdated);

            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<PatchCompany> UpdatePartiallyCompanyAsync(Guid id, PatchCompany company)
        {
            var companyFromDB = await GetCompanyByIdAsync(id);
            if (companyFromDB == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(company.companyname) && company.companyname != companyFromDB.companyname)
            {
                companyFromDB.companyname = company.companyname;
            }
            if (!string.IsNullOrEmpty(company.companyemail) && company.companyemail != companyFromDB.companyemail)
            {
                companyFromDB.companyemail = company.companyemail;
            }
            if (!string.IsNullOrEmpty(company.companycui) && company.companycui != companyFromDB.companycui)
            {
                companyFromDB.companycui = company.companycui;
            }

            if (company.dateadded.HasValue && company.dateadded != companyFromDB.dateadded)
            {
                companyFromDB.dateadded = company.dateadded;
            }

            if (company.active != companyFromDB.active)
            {
                companyFromDB.active = company.active;
            }
            _context.Companys.Update(companyFromDB);
            await _context.SaveChangesAsync();
            return company;





        }
        private async Task<bool> ExistCompanyAsync(Guid id)
        {
            return await _context.Companys.CountAsync(a => a.companyid == id) > 0;
        }
    }
}
