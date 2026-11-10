using System.Security.Cryptography;
using UrlShortener.Domain.Services;

namespace UrlShortener.Infrastructure.Services;

internal sealed class ShortCodeGenerator : IShortCodeGenerator
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const int CodeLength = 7;

    public string Generate() => RandomNumberGenerator.GetString(Alphabet, CodeLength);
}