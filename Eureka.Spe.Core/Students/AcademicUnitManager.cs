using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Eureka.Spe.NewsFeed.Entities;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Students
{
    public class AcademicUnitManager :DomainService, IAcademicUnitManager
    {
        private readonly IRepository<Feed> _feedRepository;
        private readonly IRepository<AcademicUnit> _academicUnitsRepository;
        public AcademicUnitManager(IRepository<Feed> feedRepository, IRepository<AcademicUnit> academicUnitsRepository)
        {
            _feedRepository = feedRepository;
            _academicUnitsRepository = academicUnitsRepository;
        }

        public async Task AddAcademicUnitsToEntity(string entityName, int entityId, int[] ids)
        {
            switch (entityName)
            {
                case "feeds":
                    var feed = _feedRepository.GetAllIncluding(a => a.AcademicUnits).FirstOrDefault(a => a.Id == entityId);

                    if (feed != null)
                    {
                        feed.AcademicUnits = new List<AcademicUnit>();

                        foreach (var id in ids)
                        {
                            var academicunit = _academicUnitsRepository.Get(id);

                            feed.AcademicUnits.Add(academicunit);
                            
                        }


                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                    break;
            }
        }
    }
}
