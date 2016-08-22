// <copyright file="ProcessStatusesController.cs">
// This is a property of a Iurii Khrystiuk. No rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Lotto.Logic.Interfaces;
using Lotto.Model.Constants;
using Lotto.Web.Models;

namespace Lotto.Web.Controllers
{
    public class ProcessStatusesController : ApiController
    {
        private readonly ILotteryProcessStatusManager processStatusManager;
        private readonly ILotteryProcessStepsManager processStepsManager;

        public ProcessStatusesController(ILotteryProcessStatusManager processStatusManager, ILotteryProcessStepsManager processStepsManager)
        {
            this.processStatusManager = processStatusManager;
            this.processStepsManager = processStepsManager;
        }

        [HttpGet]
        public IEnumerable<ProcessStatus> Get()
        {
            return this.processStatusManager.Get().Select(l => new ProcessStatus
            {
                Id = l.Id,
                LotteryId = l.PrimaryLotteryPrize.LotteryId,
                LotteryName = l.PrimaryLotteryPrize.Lottery.Name,
                PrimaryPrizeId = l.PrimaryLotteryPrize.Id,
                Size = l.PrimaryLotteryPrize.Size,
                Status = this.convertStatus(l.Status)
            });
        }

        [HttpGet]
        public ProcessStatus Get(int id)
        {
            var processStatus = this.processStatusManager.GetForPrizeMap(id);
            var steps = this.processStepsManager.GetForStatus(processStatus.Id).ToList();
            var currentStep = steps.FirstOrDefault(s => s.Status == Status.Started);
            var seconds = steps.Select(x => x.Duration.TotalSeconds).ToList();
            var finished = steps.Count(s => s.Status == Status.Finished);
            var averageDuration = (int)seconds.Sum() / (finished == 0 ? 1 : finished);

            return new ProcessStatus
            {
                Id = processStatus.Id,
                LotteryId = processStatus.PrimaryLotteryPrize.LotteryId,
                LotteryName = processStatus.PrimaryLotteryPrize.Lottery.Name,
                PrimaryPrizeId = processStatus.PrimaryLotteryPrize.Id,
                Size = processStatus.PrimaryLotteryPrize.Size,
                Status = this.convertStatus(processStatus.Status),
                CurrentStep = currentStep == null ? (processStatus.Status == Status.Finished ? steps.Count : 0) : steps.IndexOf(currentStep),
                TotalSteps = steps.Count,
                AverageDuration = averageDuration,
                MaxDuration = seconds.Any() ? (int)seconds.Max() : 0,
                EstimatedTime = steps.Count(s => s.Status != Status.Finished) * averageDuration
            };
        }

        [HttpGet]
        public void QueueProcess([FromUri] int prizeMapId)
        {
            this.processStatusManager.SetStatus(prizeMapId, Status.Queued);
        }

        [HttpGet]
        public void StopProcess([FromUri] int prizeMapId)
        {
            this.processStatusManager.SetStatus(prizeMapId, Status.Stopping);
        }

        [HttpGet]
        public void CancelProcess([FromUri] int prizeMapId)
        {
            this.processStatusManager.SetStatus(prizeMapId, Status.Cancelling);
        }

        [HttpGet]
        public void ContinueProcess([FromUri] int prizeMapId)
        {
            this.processStatusManager.SetStatus(prizeMapId, Status.Continuing);
        }

        private string convertStatus(Status status)
        {
            switch (status)
            {
                case Status.NotRun:
                    return "Not Run";
                case Status.Cancelled:
                    return "Cancelled";
                case Status.Cancelling:
                    return "Cancelling";
                case Status.Continuing:
                    return "Continuing";
                case Status.Started:
                    return "Started";
                case Status.Stopped:
                    return "Stopped";
                case Status.Stopping:
                    return "Stopping";
                case Status.Finished:
                    return "Finished";
                case Status.Queued:
                    return "Queued";
                default:
                    return "Unknown";
            }
        }
    }
}