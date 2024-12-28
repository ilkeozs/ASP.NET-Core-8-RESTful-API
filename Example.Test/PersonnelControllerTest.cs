using Example.ApplicationLayer.Personnels;
using Example.Database.UnitofWork;
using Example.DomainLayer;
using Example.ViewModel;
using Moq;

namespace Example.Test;

public class PersonnelControllerTest
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly PersonnelService _service;

    public PersonnelControllerTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _service = new PersonnelService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task AddAsync_ValidModel_AddsPersonnelAndReturnsModelWithId()
    {
        var model = new AddPersonnelModel
        {
            BirthDate = DateTime.Now,
            DistrictId = 1,
            FullName = "Test",
            Gender = DomainLayer.Shared.GenderType.Male
        };

        var personnel = new Personnel
        {
            BirthDate = model.BirthDate,
            DistrictId = model.DistrictId,
            FullName = model.FullName,
            Gender = model.Gender
        };

        _mockUnitOfWork.Setup(u => u.Personnels.AddAsync(It.IsAny<Personnel>()))
            .ReturnsAsync((Personnel p) =>
            {
                p.Id = 1;
                return p;
            });

        _mockUnitOfWork.Setup(u => u.CompleteAsync()).Returns(Task.FromResult(0));
        _mockUnitOfWork.Setup(u => u.Personnels.AddAsync(It.Is<Personnel>(p => p == personnel)))
            .Callback<Personnel>(p => model.Id = p.Id);

        var result = await _service.AddAsync(model);

        Assert.Equal(1, result.Id);
        _mockUnitOfWork.Verify(u => u.Personnels.AddAsync(It.IsAny<Personnel>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
    }
}