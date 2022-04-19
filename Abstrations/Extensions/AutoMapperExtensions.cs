using AutoMapper;

namespace Extensions
{
    public static class AutoMapperExtensions
    {
        public static async Task<TDestination> MapAsync<TDestination, TSoruce>(this IMapper mapper, Task<TSoruce> source) =>
            mapper.Map<TDestination>(await source);
    }
}