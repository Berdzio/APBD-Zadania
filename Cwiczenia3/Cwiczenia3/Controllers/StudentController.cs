using Cwiczenia3.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentsManagement.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private const string DatabaseFile = "students.csv";

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = ReadStudentsFromDatabase();
            return Ok(students);
        }

        [HttpGet("{studentIndex}")]
        public IActionResult GetStudent(string studentIndex)
        {
            var students = ReadStudentsFromDatabase();
            var student = students.FirstOrDefault(s => s.NumberIndex == studentIndex);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPut]
        public IActionResult AddStudent(Student student)
        {
            if (!IsStudentDataValid(student))
                return BadRequest("Incomplete student data");

            var students = ReadStudentsFromDatabase();
            if (students.Any(s => s.NumberIndex == student.NumberIndex))
                return BadRequest("Student with the same index already exists");

            students.Add(student);
            WriteStudentsToDatabase(students);
            return Ok(student);
        }

        [HttpPost("{studentIndex}")]
        public IActionResult UpdateStudent(string studentIndex, Student updatedStudent)
        {
            if (!IsStudentDataValid(updatedStudent))
                return BadRequest("Incomplete student data");

            var students = ReadStudentsFromDatabase();
            var existingStudent = students.FirstOrDefault(s => s.NumberIndex == studentIndex);
            if (existingStudent == null)
                return NotFound();

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
            existingStudent.Studies = updatedStudent.Studies;
            existingStudent.Mode = updatedStudent.Mode;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.FatherName = updatedStudent.FatherName;
            existingStudent.MotherName = updatedStudent.MotherName;

            WriteStudentsToDatabase(students);
            return Ok(existingStudent);
        }

        [HttpDelete("{studentIndex}")]
        public IActionResult DeleteStudent(string studentIndex)
        {
            var students = ReadStudentsFromDatabase();
            var existingStudent = students.FirstOrDefault(s => s.NumberIndex == studentIndex);
            if (existingStudent == null)
                return NotFound();

            students.Remove(existingStudent);
            WriteStudentsToDatabase(students);
            return NoContent();
        }

        private List<Student> ReadStudentsFromDatabase()
        {
            if (!System.IO.File.Exists(DatabaseFile))
                return new List<Student>();

            var students = new List<Student>();
            using (var reader = new StreamReader(DatabaseFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var student = new Student
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        NumberIndex = values[2],
                        DateOfBirth = values[3],
                        Studies = values[4],
                        Mode = values[5],
                        Email = values[6],
                        FatherName = values[7],
                        MotherName = values[8]
                    };
                    students.Add(student);
                }
            }
            return students;
        }

        private void WriteStudentsToDatabase(List<Student> students)
        {
            using (var writer = new StreamWriter(DatabaseFile))
            {
                foreach (var student in students)
                {
                    var line = $"{student.FirstName},{student.LastName},{student.NumberIndex},{student.DateOfBirth},{student.Studies},{student.Mode},{student.Email},{student.FatherName},{student.MotherName}";
                    writer.WriteLine(line);
                }
            }
        }

        private bool IsStudentDataValid(Student student)
        {
            return !string.IsNullOrEmpty(student.FirstName)
                && !string.IsNullOrEmpty(student.LastName)
                && !string.IsNullOrEmpty(student.NumberIndex)
                && !string.IsNullOrEmpty(student.DateOfBirth)
                && !string.IsNullOrEmpty(student.Studies)
                && !string.IsNullOrEmpty(student.Mode)
                && !string.IsNullOrEmpty(student.Email)
                && !string.IsNullOrEmpty(student.FatherName)
                && !string.IsNullOrEmpty(student.MotherName);
        }
    }
}