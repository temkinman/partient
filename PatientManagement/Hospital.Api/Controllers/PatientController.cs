using AutoMapper;
using Hospital.Api.Dtos;
using Hospital.Api.Helpers;
using Hospital.Application.Dtos;
using Hospital.Application.Enums;
using Hospital.Application.Patients.Commands.CreatePatient;
using Hospital.Application.Patients.Commands.DeletePatient;
using Hospital.Application.Patients.Commands.UpdatePatient;
using Hospital.Application.Patients.Queries.GetPatientById;
using Hospital.Application.Patients.Queries.GetPatientsByBirthDate;
using Hospital.Application.Patients.Queries.GetPatientsByPeroidDates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PatientController(ILogger<PatientController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Retrieves a patient by their unique identifier.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient to retrieve.</param>
    /// <returns>
    /// A <see cref="ActionResult{PatientDto}"/> containing the patient details if found,
    /// or appropriate HTTP status codes:
    /// </returns>
    [HttpGet]
    [Route("patients/{patientId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> GetPatientById(Guid patientId)
    {
        var result = await _mediator.Send(new GetPatientByIdQuery(patientId));
        var response = _mapper.Map<PatientDto>(result.Patient);

        return Ok(response);
    }
    
    /// <summary>
    /// Searches for patients based on their birth date.
    /// </summary>
    /// <param name="birthDate">The birth date parameter used for searching patients. 
    /// The format can vary based on the prefix (e.g., "before:YYYY-MM-DD", "after:YYYY-MM-DD", "on:YYYY-MM-DD").</param>
    /// <returns>
    /// A <see cref="ActionResult{IEnumerable{PatientDto}}"/> containing a list of patients that match the search criteria,
    /// or appropriate HTTP status codes:
    /// </returns>
    [HttpGet]
    [Route("patients/search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> SearchByBirthDate(string birthDate)
    {
        (DateParamsPrefix prefix, DateTime? parsedDate) = GetBirthDateAndPrefix(birthDate);
        if (prefix == DateParamsPrefix.unknown || parsedDate == null)
        {
            return BadRequest("Invalid birth date parameter");
        }
        
        var result = await _mediator.Send(new GetPatientsByBirthDateQuery((DateTime)parsedDate, prefix));
        var response = _mapper.Map<IEnumerable<PatientDto>>(result.Patients);

        return Ok(response);
    }
    
    /// <summary>
    /// Searches for patients based on a specified period defined by start and end birth dates.
    /// </summary>
    /// <param name="startDate">The start date parameter used for searching patients. 
    /// The format can vary based on the prefix (e.g., "before:YYYY-MM-DD", "after:YYYY-MM-DD", "on:YYYY-MM-DD").</param>
    /// <param name="endDate">The end date parameter used for searching patients. 
    /// The format can vary based on the prefix (e.g., "before:YYYY-MM-DD", "after:YYYY-MM-DD", "on:YYYY-MM-DD").</param>
    /// <returns>
    /// A <see cref="ActionResult{IEnumerable{PatientDto}}"/> containing a list of patients that match the search criteria,
    /// </returns>
    [HttpGet]
    [Route("patients/period-search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PatientDto>>> PeriodSearchByDates(string startDate, string endDate)
    {
        (DateParamsPrefix prefixStart, DateTime? parsedStartDate) = GetBirthDateAndPrefix(startDate);
        (DateParamsPrefix prefixEnd, DateTime? parsedEndDate) = GetBirthDateAndPrefix(endDate);
        
        if (prefixStart == DateParamsPrefix.unknown || prefixEnd == DateParamsPrefix.unknown 
            || parsedStartDate == null || parsedEndDate == null)
        {
            return BadRequest("Invalid birth date's period parameters");
        }
        
        var result = await _mediator.Send(new GetPatientsByPeriodDatesQuery(prefixStart, prefixEnd, (DateTime)parsedStartDate, (DateTime)parsedEndDate));
        var response = _mapper.Map<IEnumerable<PatientDto>>(result.Patients);

        return Ok(response);
    }
    
    /// <summary>
    /// Creates a new patient record in the system.
    /// </summary>
    /// <param name="request">The request object containing the details of the patient to be created.</param>
    /// <returns>
    /// A <see cref="ActionResult{Guid}"/> representing the unique identifier of the newly created patient.
    /// </returns>
    [HttpPost]
    [Route("patients")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePatient(CreatePatientRequest request)
    {
        request.BirthDate = DateTimeHelper.GetDateTimeWithUtc(request.BirthDate);
        
        var command = _mapper.Map<CreatePatientCommand>(request);
        var result = await _mediator.Send(command);

        return Ok(result.PatientId);
    }
    
    /// <summary>
    /// Updates an existing patient record in the system.
    /// </summary>
    /// <param name="request">The request object containing the updated details of the patient.</param>
    /// <returns>
    /// A <see cref="ActionResult{PatientDto}"/> representing the updated patient details.
    /// </returns>
    [HttpPut]
    [Route("patients")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PatientDto>> UpdatePatient(UpdatePatientRequest request)
    {
        request.BirthDate = DateTimeHelper.GetDateTimeWithUtc(request.BirthDate);
        
        var command = _mapper.Map<UpdatePatientCommand>(request);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<PatientDto>(result.Patient);

        return Ok(response);
    }
    
    /// <summary>
    /// Deletes a patient record from the system by its unique identifier.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient to be deleted.</param>
    /// <returns>
    /// A <see cref="ActionResult{bool}"/> indicating whether the deletion was successful.
    /// </returns>
    [HttpDelete]
    [Route("patients/{patientId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteProduct(Guid patientId)
    {
        var result = await _mediator.Send(new DeletePatientCommand(patientId));
        
        return Ok(result.IsSuccess);
    }

    private (DateParamsPrefix, DateTime? date) GetBirthDateAndPrefix(string birthDate)
    {
        if (string.IsNullOrWhiteSpace(birthDate) || birthDate.Length < 2)
        {
            return (DateParamsPrefix.unknown, null);
        }

        string prefix = birthDate.Substring(0, 2);

        if (!Enum.TryParse(prefix, out DateParamsPrefix datePrefix))
        {
            return (DateParamsPrefix.unknown, null);
        }
        
        string dateStr = birthDate.Substring(2);

        if (!DateTime.TryParse(dateStr, out DateTime date))
        {
            return (DateParamsPrefix.unknown, null);
        }
        
        date = DateTimeHelper.GetDateTimeWithUtc(date);

        return (datePrefix, date);
    }
}