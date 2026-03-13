using Core;
using FluentAssertions;
using RestWithASPNET10.Data;
using RestWithASPNET10.Data.Converter;

namespace xUnit.Tests
{
    public class PersonConverterTests
    {
        private readonly PersonConverter _personConverter;

        public PersonConverterTests()
        {
            _personConverter = new PersonConverter();
        }

        //- PersonDTO to Person conversion tests
        [Fact]
        public void Parse_ShouldConvertPersonDTOToPerson()
        {

            // Arrange: prepare the data, objects and dependencies necessary for the test;
            var dto = new PersonDTO
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                Gender = "Male",
            };

            var expectedPerson = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                Gender = "Male",
            };

            // Act: execute the method or functionality being tested;
            var person = _personConverter.Parse(dto);

            // Assert: verify that the expected results match the actual results.
            person.Should().NotBeNull();

            person.Id.Should().Be(expectedPerson.Id);
            person.FirstName.Should().Be(expectedPerson.FirstName);
            person.LastName.Should().Be(expectedPerson.LastName);
            person.Address.Should().Be(expectedPerson.Address);
            person.Gender.Should().Be(expectedPerson.Gender);

            person.Should().BeEquivalentTo(expectedPerson);

        }

        [Fact]
        public void Parse_PersonDTOShouldReturnNull()
        {
            // Arrange
            PersonDTO dto = null;
            // Act
            var person = _personConverter.Parse(dto);
            // Assert
            person.Should().BeNull();
        }

        [Fact]
        public void Parse_ShouldConvertPersonToPersonDTO()
        {

            // Arrange: prepare the data, objects and dependencies necessary for the test;
            var entity = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                Gender = "Male",
            };

            var expectedPerson = new PersonDTO
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                //Gender = "Male",
            };

            // Act: execute the method or functionality being tested;
            var person = _personConverter.Parse(entity);

            // Assert: verify that the expected results match the actual results.
            person.Should().NotBeNull();

            person.Id.Should().Be(expectedPerson.Id);
            person.FirstName.Should().Be(expectedPerson.FirstName);
            person.LastName.Should().Be(expectedPerson.LastName);
            person.Address.Should().Be(expectedPerson.Address);
            //person.Gender.Should().Be(expectedPerson.Gender);

            person.Should().BeEquivalentTo(expectedPerson,
                                           options => options.Excluding(person => person.Gender));
        }

        [Fact]
        public void Parse_PersonShouldReturnNull()
        {
            // Arrange
            Person entity = null;
            // Act
            var person = _personConverter.Parse(entity);
            // Assert
            person.Should().BeNull();
        }

        [Fact]
        public void ParseList_ShouldConvertPersonDTOListToPersonList()
        {
            // Arrange
            var dtoList = new List<PersonDTO>
            {
                new PersonDTO {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Main",
                    Gender = "Male",
                },
                new PersonDTO {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Address = "456 Elm",
                    Gender = "Female",
                }
            };

            //Act
            var personList = _personConverter.ParseList(dtoList);

            // Assert
            personList.Should().NotBeNull();
            personList.Should().HaveCount(dtoList.Count);
            personList[0].Should().BeEquivalentTo(new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                Gender = "Male",
            });
            personList[1].Should().BeEquivalentTo(new Person
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Elm",
                Gender = "Female",
            });
        }

        [Fact]
        public void ParseList_PersonDTOListShouldReturnNull()
        {
            // Arrange
            List<PersonDTO> dtoList = null;
            // Act
            var personList = _personConverter.ParseList(dtoList);
            // Assert
            personList.Should().BeNull();
        }

        [Fact]
        public void ParseList_ShouldConvertPersonListToPersonDTOList()
        {
            // Arrange
            var dtoList = new List<Person>
            {
                new Person {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Main",
                    Gender = "Male",
                },
                new Person {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Address = "456 Elm",
                    Gender = "Female",
                }
            };

            //Act
            var personList = _personConverter.ParseList(dtoList);

            // Assert
            personList.Should().NotBeNull();
            personList.Should().HaveCount(dtoList.Count);
            personList[0].Should().BeEquivalentTo(new PersonDTO
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main",
                Gender = "Male",
            });
            personList[1].Should().BeEquivalentTo(new PersonDTO
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Elm",
                Gender = "Female",
            });
        }

        [Fact]
        public void ParseList_PersonListShouldReturnNull()
        {
            // Arrange
            List<Person> entityList = null;
            // Act
            var personList = _personConverter.ParseList(entityList);
            // Assert
            personList.Should().BeNull();
        }
    }
}