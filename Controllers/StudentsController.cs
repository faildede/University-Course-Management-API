using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.Common;
using Student.DTOs;
using Student.Models;
using Repositories.Interfaces;

namespace Student.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student.Models.Student> _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IRepository<Student.Models.Student> studentRepository, IMapper mapper, ILogger<StudentsController> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ReturnResult<IEnumerable<StudentDto>>>> GetAll()
        {
            _logger.LogInformation("Запрос на получение всех студентов.");
            var students = await _studentRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(ReturnResult<IEnumerable<StudentDto>>.Success(dtos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnResult<StudentDto>>> GetById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Студент с ID {Id} не найден.", id);
                return NotFound(ReturnResult<StudentDto>.Fail(404, "STUDENT_NOT_FOUND", "Student not found"));
            }

            return Ok(ReturnResult<StudentDto>.Success(_mapper.Map<StudentDto>(student)));
        }

        [HttpPost]
        public async Task<ActionResult<ReturnResult<StudentDto>>> Create([FromBody] StudentCreateDto dto)
        {
            var student = _mapper.Map<Student.Models.Student>(dto);
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            _logger.LogInformation("Создан новый студент с ID {Id}.", student.Id);
            var resultDto = _mapper.Map<StudentDto>(student);
            
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, ReturnResult<StudentDto>.Success(resultDto, 201));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReturnResult<bool>>> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Попытка удаления несуществующего студента с ID {Id}.", id);
                return NotFound(ReturnResult<bool>.Fail(404, "STUDENT_NOT_FOUND", "Student not found"));
            }

            _studentRepository.Delete(student);
            await _studentRepository.SaveChangesAsync();

            _logger.LogInformation("Удален студент с ID {Id}.", id);
            return Ok(ReturnResult<bool>.Success(true));
        }
    }
}