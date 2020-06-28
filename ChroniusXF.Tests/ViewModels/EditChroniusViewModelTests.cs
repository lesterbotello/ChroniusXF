﻿using AutoFixture;
using ChroniusXF.DataModels;
using ChroniusXF.Persistence;
using ChroniusXF.ViewModels;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Xunit;

namespace ChroniusXF.Tests.ViewModels
{
    public class EditChroniusViewModelTests
    {
        [Fact(DisplayName = "SaveCommand when Chronius saved successfully shoud reload data and navigate back")]
        public void SaveCommand_WhenChroniusSavedSuccessfully_ShoudReloadDataAndNavigateBack()
        {
            // Arrange:
            var fixture = new Fixture();
            var chronius = fixture.Create<Chronius>();
            var navigationServiceMock = new Mock<INavigationService>();
            var pageDialogServiceMock = new Mock<IPageDialogService>();
            var chroniusDatabaseMock = new Mock<IChroniusDatabase>();
            chroniusDatabaseMock.Setup(x => x.SaveChroniusAsync(chronius)).ReturnsAsync(1);
            var viewModel = new EditChroniusViewModel(navigationServiceMock.Object, pageDialogServiceMock.Object, chroniusDatabaseMock.Object);
            var navParams = new NavigationParameters
            {
                    { "chronius", chronius },
                    { "parentViewModel", null }
            };

            // Act:
            viewModel.SaveCommand.Execute();

            // Assert:
            navigationServiceMock.Verify(x => x.GoBackAsync(), Times.Once);
        }
    }
}