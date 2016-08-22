using System;
using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotteryProcessStepsManager : ILotteryProcessStepsManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotteryProcessStepsManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public IEnumerable<LotteryProcessStep> AddRange(IEnumerable<LotteryProcessStep> steps)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var stepRepo = uow.GetRepository<IRepository<LotteryProcessStepRecord>, LotteryProcessStepRecord>();
                var records = new List<LotteryProcessStepRecord>();
                foreach (var lotteryProcessStep in steps)
                {
                    var record = new LotteryProcessStepRecord
                    {
                        Description = lotteryProcessStep.Description,
                        Duration = lotteryProcessStep.Duration,
                        EndDate = lotteryProcessStep.EndDate,
                        LotteryProcessStatusId = lotteryProcessStep.LotteryProcessStatusId,
                        StartDate = lotteryProcessStep.StartDate,
                        Status = lotteryProcessStep.Status
                    };
                    stepRepo.Add(record);
                    records.Add(record);
                }

                uow.SaveChanges();

                return records.Select(r => new LotteryProcessStep
                {
                    Id = r.Id,
                    Description = r.Description,
                    Duration = r.Duration,
                    EndDate = r.EndDate,
                    LotteryProcessStatusId = r.LotteryProcessStatusId,
                    StartDate = r.StartDate,
                    Status = r.Status
                });
            }
        }

        public void Update(LotteryProcessStep currentStep)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var stepRepo = uow.GetRepository<IRepository<LotteryProcessStepRecord>, LotteryProcessStepRecord>();
                var step = stepRepo.First(r => r.Id == currentStep.Id);
                step.Description = currentStep.Description;
                step.Duration = currentStep.Duration;
                step.EndDate = currentStep.EndDate;
                step.LotteryProcessStatusId = currentStep.LotteryProcessStatusId;
                step.StartDate = currentStep.StartDate;
                step.Status = currentStep.Status;
                stepRepo.Update(step);
                uow.SaveChanges();
            }
        }

        public IEnumerable<LotteryProcessStep> GetForStatus(int statusId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var stepRepo = uow.GetRepository<IRepository<LotteryProcessStepRecord>, LotteryProcessStepRecord>();
                return stepRepo.Where(s => s.LotteryProcessStatusId == statusId).Select(s => new LotteryProcessStep
                {
                    Id = s.Id,
                    Duration = s.Duration,
                    Status = s.Status
                }).ToList();
            }
        }

        public void ClearForStatus(int statusId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var stepRepo = uow.GetRepository<IRepository<LotteryProcessStepRecord>, LotteryProcessStepRecord>();
                foreach (var lotteryProcessStepRecord in stepRepo.Where(s => s.LotteryProcessStatusId == statusId))
                {
                    stepRepo.Delete(lotteryProcessStepRecord);
                }
                uow.SaveChanges();
            }
        }
    }
}
