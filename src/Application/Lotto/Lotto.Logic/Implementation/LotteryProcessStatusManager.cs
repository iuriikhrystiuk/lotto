// <copyright file="LotteryProcessStatusManager.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using IK.DataAccess.Interfaces;
using Lotto.Logic.Interfaces;
using Lotto.Model.Constants;
using Lotto.Model.Entities.Hub;
using Lotto.Model.Implementation.Hub;
using Lotto.Model.Records.Hub;

namespace Lotto.Logic.Implementation
{
    public class LotteryProcessStatusManager : ILotteryProcessStatusManager
    {
        private readonly IUnitOfWorkFactory uowFactory;

        public LotteryProcessStatusManager(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        public IEnumerable<LotteryProcessStatus> Get()
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var processSourceRepo =
                    uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                var prizeMapRepo =
                    uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var lotteryRepo =
                    uow.GetRepository<IRepository<LotteryRecord>, LotteryRecord>();
                var processStatusRepo = uow.GetRepository<IRepository<LotteryProcessStatusRecord>, LotteryProcessStatusRecord>();
                var prizeMaps = prizeMapRepo.Where(p => p.NextLotteryPrizeId == null);
                return (from prizeMap in prizeMaps
                        let processSource = processSourceRepo.FirstOrDefault(p => p.PrimaryLotteryPrize.Id == prizeMap.Id)
                        where processSource != null
                        let lottery = lotteryRepo.First(l => l.Id == prizeMap.LotteryId)
                        let processStatus =
                            processStatusRepo.FirstOrDefault(p => p.PrimaryLotteryPrizeId == prizeMap.Id)
                        select new LotteryProcessStatus
                        {
                            Id = processStatus == null ? 0 : processStatus.Id,
                            PrimaryLotteryPrizeId = prizeMap.Id,
                            PrimaryLotteryPrize = new LotteryPrizeMap
                            {
                                Id = prizeMap.Id,
                                Size = prizeMap.Size,
                                LotteryId = lottery.Id,
                                Lottery = new Lottery
                                {
                                    Id = lottery.Id,
                                    Name = lottery.Name
                                },
                                ProcessSource = new LotteryProcessSource
                                {
                                    ConnectionString = processSource.ConnectionString
                                }
                            },
                            Status = processStatus == null ? Status.NotRun : processStatus.Status
                        }).ToList();
            }
        }

        public LotteryProcessStatus GetForPrizeMap(int prizeMapId)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var processSourceRepo =
                    uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                var prizeMapRepo =
                    uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var lotteryRepo =
                    uow.GetRepository<IRepository<LotteryRecord>, LotteryRecord>();
                var processStatusRepo =
                    uow.GetRepository<IRepository<LotteryProcessStatusRecord>, LotteryProcessStatusRecord>();
                var prizeMap = prizeMapRepo.First(p => p.Id == prizeMapId);
                var processSource = processSourceRepo.FirstOrDefault(p => p.PrimaryLotteryPrize.Id == prizeMap.Id);
                if (processSource != null)
                {
                    var lottery = lotteryRepo.First(l => l.Id == prizeMap.LotteryId);
                    var processStatus =
                        processStatusRepo.FirstOrDefault(p => p.PrimaryLotteryPrizeId == prizeMap.Id);
                    return new LotteryProcessStatus
                    {
                        Id = processStatus == null ? 0 : processStatus.Id,
                        PrimaryLotteryPrizeId = prizeMap.Id,
                        PrimaryLotteryPrize = new LotteryPrizeMap
                        {
                            Id = prizeMap.Id,
                            Size = prizeMap.Size,
                            Lottery = new Lottery
                            {
                                Name = lottery.Name
                            }
                        },
                        Status = processStatus == null ? Status.NotRun : processStatus.Status
                    };
                }

                return null;
            }
        }

        public void SetStatus(int prizeMapId, Status status)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork(new HubContextDescriptor()))
            {
                var processSourceRepo =
                    uow.GetRepository<IRepository<LotteryProcessSourceRecord>, LotteryProcessSourceRecord>();
                var prizeMapRepo =
                    uow.GetRepository<IRepository<LotteryPrizeMapRecord>, LotteryPrizeMapRecord>();
                var processStatusRepo =
                    uow.GetRepository<IRepository<LotteryProcessStatusRecord>, LotteryProcessStatusRecord>();
                var prizeMap = prizeMapRepo.First(p => p.Id == prizeMapId);
                var processSource = processSourceRepo.First(p => p.PrimaryLotteryPrize.Id == prizeMap.Id);
                var processStatus =
                    processStatusRepo.FirstOrDefault(p => p.PrimaryLotteryPrizeId == prizeMap.Id);
                if (processStatus == null)
                {
                    processStatusRepo.Add(new LotteryProcessStatusRecord
                    {
                        PrimaryLotteryPrizeId = prizeMap.Id,
                        Status = status
                    });
                }
                else
                {
                    processStatus.Status = status;
                    processStatusRepo.Update(processStatus);
                }
                uow.SaveChanges();
            }
        }
    }
}
