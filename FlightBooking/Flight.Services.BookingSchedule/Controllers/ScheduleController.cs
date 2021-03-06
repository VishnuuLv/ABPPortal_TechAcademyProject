using Flight.Services.BookingSchedule.Models.Dto;
using Flight.Services.BookingSchedule.RabbitMQSender;
using Flight.Services.BookingSchedule.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.Services.BookingSchedule.Controllers
{
    [Route("api/Schedule")]
    [AllowAnonymous]

    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        protected ResponseDto _response;
        private readonly IRabbitMQAirlineSender _rabbitMQairlineSender;

        public ScheduleController(IScheduleRepository scheduleRepository, IRabbitMQAirlineSender rabbitMQairlineSender)
        {
            _scheduleRepository = scheduleRepository;
            this._response = new ResponseDto();
            _rabbitMQairlineSender = rabbitMQairlineSender;
        }

        [HttpGet("GetSchedule")]
        
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ScheduleDetailViewDto> scheduleDt = await _scheduleRepository.GetSchedule();
                _response.Result = scheduleDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpGet("GetAllSchedule")]
        
        public async Task<object> GetAllSchedule()
        {
            try
            {
                IEnumerable<ScheduleDetailViewDto> scheduleDt = await _scheduleRepository.GetAllSchedule();
                _response.Result = scheduleDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpGet]
        
        [Route("GetScheduleByID/{scheduleDetailsId}")]
        public async Task<object> GetScheduleByID(int scheduleDetailsId)
        {
            try
            {
                ScheduleDetailViewDto scheduleDt = await _scheduleRepository.GetScheduleById(scheduleDetailsId);
                _response.Result = scheduleDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
        [HttpPost]
        
        [Route("AddSchedule")]

        public async Task<object> AddSchedule([FromBody] ScheduleDetailDto scheduleDetailsDto)
        {
            try
            {
                ScheduleDetailDto scheduleDt = await _scheduleRepository.CreateUpdateSchedule(scheduleDetailsDto);
                _response.Result = scheduleDt;

                //ScheduleDetailViewDto schedule = await _scheduleRepository.GetScheduleById(scheduleDt.scheduleDetailsId);
                //ScheduleDetailViewDto schedule1 = schedule;

                LogsDto log = new LogsDto();
                log.log = "New Schedule was added for the flight ID " + scheduleDetailsDto.flightId +" from "+scheduleDetailsDto.fromPlace+" to "+scheduleDetailsDto.toPlace+ " by Admin";
                log.task = "Create";
                log.senderAPI = "BookingScheduleAPI";


                //rabbitMQ
                _rabbitMQairlineSender.SendData(log, "logqueue");
               // _rabbitMQairlineSender.SendData(schedule1, "scheduledataqueue");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        
        [Route("UpdateSchedule")]
        public async Task<object> UpdateSchedule([FromBody] ScheduleDetailDto scheduleDetailsDto)
        {
            try
            {
                ScheduleDetailDto scheduleDt = await _scheduleRepository.CreateUpdateSchedule(scheduleDetailsDto);
                _response.Result = scheduleDt;

                ScheduleDetailViewDto schedule = await _scheduleRepository.GetScheduleById(scheduleDt.scheduleDetailsId);
                ScheduleDetailViewDto schedule1 = schedule;

                LogsDto log = new LogsDto();
                log.log = schedule1.scheduleDetailsId + " - Schedule was updated by Admin";
                log.task = "Update";
                log.senderAPI = "BookingScheduleAPI";


                //rabbitMQ
                _rabbitMQairlineSender.SendData(log, "logqueue");
                // _rabbitMQairlineSender.SendData(schedule1, "scheduledataqueue");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        
        [Route("RemoveSchedule/{ScheduleDetailsId}")]
        public async Task<object> RemoveSchedule(int ScheduleDetailsId)
        {
            try
            {
                bool isSuccess = await _scheduleRepository.RemoveSchedule(ScheduleDetailsId);
                _response.Result = isSuccess;

                ScheduleDetailViewDto schedule = await _scheduleRepository.GetScheduleById(ScheduleDetailsId);
                ScheduleDetailViewDto schedule1 = schedule;

                LogsDto log = new LogsDto();
                log.log = schedule1.scheduleDetailsId + " - Schedule was deleted by Admin";
                log.task = "Delete";
                log.senderAPI = "BookingScheduleAPI";


                //rabbitMQ
                _rabbitMQairlineSender.SendData(log, "logqueue");
                // _rabbitMQairlineSender.SendData(schedule1, "scheduledataqueue");

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        
        [Route("{date:DateTime}")]
        public async Task<object> Get(DateTime date)
        {
            try
            {
                IEnumerable<ScheduleDetailViewDto> scheduleDtos = await _scheduleRepository.GetScheduleByDate(date);
                _response.Result = scheduleDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
        [HttpGet]
        
        [Route("{source}/{destination}")]
        public async Task<object> Get(string source, string destination)
        {
            try
            {
                IEnumerable<ScheduleDetailViewDto> scheduleDtos = await _scheduleRepository.GetScheduleBySourceDestination(source, destination);
                _response.Result = scheduleDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
        [HttpGet]
        
        [Route("{source}/{destination}/{traveldate:Datetime}")]
        public async Task<object> Get(string source, string destination, DateTime traveldate)
        {
            try
            {
                IEnumerable<ScheduleDetailViewDto> scheduleDtos = await _scheduleRepository.GetScheduleBySDwithDate(source, destination, traveldate);
                _response.Result = scheduleDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }
    }
}
