using Share.Helper;

namespace Share.Dtos;

public sealed record ListDto(int Count, IEnumerable<IResponse> Responses);