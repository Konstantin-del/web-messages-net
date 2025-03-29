
using Messages.Bll.Interfaces;
using Moq;
using Moq.Protected;
using System.Net;


namespace Messages.Bll.Tests;

public class RedirectUserRequestServiceTests
{
    private IRedirectUserRequestService _sut;
    private Mock<HttpMessageHandler> _messageHandlerMock;
    private string _baseAddress = "https://jsonplaceholder.typicode.com";

    public RedirectUserRequestServiceTests()
    {
        _messageHandlerMock = new Mock<HttpMessageHandler>();
        _sut = new RedirectUserRequestService(_messageHandlerMock.Object);
    }

    [Fact]
    public async Task GetUserFromPlaceholderAsync_CorrectUserIdPassed_SuccessReceived()
    {
        // arange
        var id = 1;
        var apiEndpoint = $"/users/{id}";
        var mockedProtected = _messageHandlerMock.Protected(); // позволяет вызывать protected методы
        var response = "{\r\n    \"id\": 1,\r\n    \"name\": \"Leanne Graham\",\r\n    \"username\": \"Bret\",\r\n    \"email\": \"Sincere@april.biz\",\r\n    \"address\": {\r\n      \"street\": \"Kulas Light\",\r\n      \"suite\": \"Apt. 556\",\r\n      \"city\": \"Gwenborough\",\r\n      \"zipcode\": \"92998-3874\",\r\n      \"geo\": {\r\n        \"lat\": \"-37.3159\",\r\n        \"lng\": \"81.1496\"\r\n      }\r\n    },\r\n    \"phone\": \"1-770-736-8031 x56442\",\r\n    \"website\": \"hildegard.org\",\r\n    \"company\": {\r\n      \"name\": \"Romaguera-Crona\",\r\n      \"catchPhrase\": \"Multi-layered client-server neural-net\",\r\n      \"bs\": \"harness real-time e-markets\"\r\n    }\r\n  }";
        var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.//IsAny<HttpRequestMessage>(),
                Is<HttpRequestMessage>(
                t => t.RequestUri!.Equals(_baseAddress + apiEndpoint)),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            }
        );
        // act
        await _sut.GetUserFromJsonPlaceholderAsync(id);
        // assert

    }
}
