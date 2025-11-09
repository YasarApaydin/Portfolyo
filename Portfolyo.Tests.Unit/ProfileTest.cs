using AutoMapper;
using ErrorOr;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Portfolyo.Business.Features.Profiles.CreateProfile;
using Portfolyo.Business.Mapping;
using Portfolyo.DataAccess.Context;
using Portfolyo.DataAccess.Repositories;
using Portfolyo.Entities.Repositories;
using System;



namespace Portfolyo.Tests.Unit
{
    public sealed class ProfileTest
    {




        [Fact]
        public async Task Create_Should_Have_Validation_Error_When_FullName_Is_Short()
        {
           
            var validator = new CreateProfileCommandValidator();
            var request = new CreateProfileCommand(
                "Ya",
                "Title",
                "Kısa bir biyografi",
                "mail@example.com",
                "github.com/test",
                "linkedin.com/test"
            );

            
            var result = await validator.ValidateAsync(request);

            
            result.Errors.Should().Contain(x => x.PropertyName == "FullName");

        }

        [Fact]
        public async Task Create_Should_Have_Validation_Error_When_Title_Is_Short()
        {
            
            var validator = new CreateProfileCommandValidator();
            var request = new CreateProfileCommand("Yaşar", "T", "Kısa Bir biyografi", "apaydinyasar0@gmail.com", "github.com/test", "Linkedin.com/test");



            var result = await validator.ValidateAsync(request);


            
            result.Errors.Should().Contain(x => x.PropertyName == "Title");
        }

        [Fact]
        public async Task Create_Should_Have_Error_When_Email_Is_Invalid()
        {
            var validator = new CreateProfileCommandValidator();
            var request = new CreateProfileCommand("Yaşar","Title","Kısa bir biyografi","invalid", "github.com/test", "Linkedin.com/test");
        
            var result = await validator.ValidateAsync(request);

    
            result.Errors.Should().Contain(x => x.PropertyName == "Email");
        
        }

        [Fact]
        public async Task Create_Should_Pass_Validation_When_All_Fields_Are_Valid()
        {
           
            var validator = new CreateProfileCommandValidator();
            var request = new CreateProfileCommand("Yaşar","Title","Kısa bir biyografi","apaydinysar0@gmail.com", "github.com/test", "Linkedin.com/test");


            
            var rsult = await validator.ValidateAsync(request);

            
            rsult.IsValid.Should().BeTrue();

        }


        [Fact]
        public async Task Handle_Should_Call_AddAsync_And_SaveChanges()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IProfileRepository>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Portfolyo.Entities.Models.Profile>(It.IsAny<CreateProfileCommand>()))
            .Returns(new Portfolyo.Entities.Models.Profile());


            var handler = new CreateProfileCommandHandler(unitOfWorkMock.Object,repoMock.Object,mapperMock.Object);


            var request = new CreateProfileCommand(
                "Yaşar Apaydın",
                "Yazılım Geliştirici",
                "Uzun Biyografi Metni",
                "apaydinyasar0@gmail.com",
                "github.com/yasar",
                "linkedin.com/yasar"
                );



            var result = await handler.Handle(request,default);

            repoMock.Verify(r => r.AddAsync(It.IsAny<Portfolyo.Entities.Models.Profile>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }



     

    }
}
