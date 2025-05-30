﻿using BethanysPieShop.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;

namespace BethanysPieShopTest.TagHelpers;

public class EmailTagHelperTest
{
    [Fact]
    public void Generate_Email_Link()
    {
        // Arrange
        EmailTagHelper emailTagHelper = new EmailTagHelper()
        {
            Address = "test@bethanyspieshop.com",
            Content = "Email"
        };

        var tagHelperContext = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            string.Empty);

        var content = new Mock<TagHelperContent>();

        var tagHelperOutput = new TagHelperOutput(
            "a",
            new TagHelperAttributeList(),
            (cache, encoder) => Task.FromResult(content.Object));

        // Act
        emailTagHelper.Process(tagHelperContext, tagHelperOutput);

        // Assert
        Assert.Equal("Email", tagHelperOutput.Content.GetContent());
        Assert.Equal("a", tagHelperOutput.TagName);
        Assert.Equal("mailto:test@bethanyspieshop.com", tagHelperOutput.Attributes[0].Value);
    }
}
