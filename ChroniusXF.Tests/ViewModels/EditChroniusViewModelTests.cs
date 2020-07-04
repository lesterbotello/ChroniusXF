using AutoFixture;
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
            var updateableHomeScreen = new Mock<IUpdateableHomeScreen>();
            chroniusDatabaseMock.Setup(x => x.SaveChroniusAsync(chronius)).ReturnsAsync(1);
            var viewModel = new EditChroniusViewModel(navigationServiceMock.Object, pageDialogServiceMock.Object, chroniusDatabaseMock.Object);
            var navParams = new NavigationParameters
            {
                    { "chronius", chronius },
                    { "parentViewModel", updateableHomeScreen.Object }
            };
            viewModel.Initialize(navParams);

            // Act:
            viewModel.SaveCommand.Execute();

            // Assert:
            updateableHomeScreen.Verify(x => x.ReloadData(), Times.Once);
            navigationServiceMock.Verify(x => x.GoBackAsync(), Times.Once);
        }

        [Theory(DisplayName = "NavigateToAddLocation_WhenEventTypeAllowsNavigation_ShouldNavigateToAddLocationPage")]
        [InlineData(EventType.Meeting)]
        [InlineData(EventType.Party)]
        [InlineData(EventType.Seminar)]
        public void NavigateToAddLocation_WhenEventTypeAllowsNavigation_ShouldNavigateToAddLocationPage(EventType eventType)
        {
            // Arrange:
            var fixture = new Fixture();
            var navigationServiceMock = new Mock<INavigationService>();
            var pageDialogServiceMock = new Mock<IPageDialogService>();
            var viewModel = new EditChroniusViewModel(navigationServiceMock.Object, pageDialogServiceMock.Object, null)
            {
                Chronius = fixture.Build<Chronius>().With(c => c.EventTypeId, (int) eventType).Create()
            };
            
            // Act:
            viewModel.NavigateToAddLocation.Execute();
            
            // Assert:
            navigationServiceMock.Verify(x => x.NavigateAsync("AddLocation"), Times.Once);
        }
        
        [Theory(DisplayName = "NavigateToAddLocation_WhenEventTypeDoesNotAllowNavigation_ShouldNavigateToAddLocationPage")]
        [InlineData(EventType.Anniversary)]
        [InlineData(EventType.Birthday)]
        [InlineData(EventType.Reminder)]
        [InlineData(EventType.OnlineMeeting)]
        public void NavigateToAddLocation_WhenEventTypeDoesNotAllowNavigation_ShouldNavigateToAddLocationPage(EventType eventType)
        {
            // Arrange:
            var fixture = new Fixture();
            var navigationServiceMock = new Mock<INavigationService>();
            var pageDialogServiceMock = new Mock<IPageDialogService>();
            var viewModel = new EditChroniusViewModel(navigationServiceMock.Object, pageDialogServiceMock.Object, null)
            {
                Chronius = fixture.Build<Chronius>().With(c => c.EventTypeId, (int) eventType).Create()
            };
            
            // Act:
            viewModel.NavigateToAddLocation.Execute();
            
            // Assert:
            pageDialogServiceMock.Verify(x => x.DisplayAlertAsync("Chronius", "This event type doesn't need a location.", "Ok"), Times.Once);
        }
    }
}
