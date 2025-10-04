using Biblioteca.Api.Controllers;
using Biblioteca.Application.DTOS;
using Biblioteca.Domain.BackOffice.Entities;
using Biblioteca.TestUnit.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.TestUnit.Api.Controllers.BookRepository;

public class PostTestUnit
{
    private readonly BooksController controller;

    public PostTestUnit()
    {
        controller = new BooksController(new FakeUnitOfWork());
    }

    [Fact]
    public async Task PostBookOk_Return_CreatedResult()
    {
        //arrange
        var bookDTO = new BookCreateDTO()
        {
            Title = "Kaiky eos kaiky",
            Author = new Author("kaiky"),
            Price = 10
        };
        //act
        var data = await controller.PostAsync(bookDTO);
        //assert
        Assert.IsType<CreatedResult>(data);
    }
    [Fact]
    public async Task PostBookNull_Return_BadRequestResult()
    {
        //arrange
        BookCreateDTO bookDTO = null;
        //act
        var data = await controller.PostAsync(bookDTO);
        //assert
        Assert.IsType<BadRequestResult>(data);
    }
    [Fact]
    public async Task PostBookExist_Return_NotFoundResult()
    {
        //arrange
        var bookDTO = new BookCreateDTO()
        {
            Title = "Introdução ao C# e .NET",
            Author = new Author("José Silva"),
            Price = 99
        };
        //act
        var data = await controller.PostAsync(bookDTO);
        //assert
        Assert.IsType<NotFoundResult>(data);
    }
}